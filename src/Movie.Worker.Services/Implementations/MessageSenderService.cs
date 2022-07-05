using Microsoft.EntityFrameworkCore;
using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class MessageSenderService : IMessageSenderService
    {
        private IBaseRepository _repository;

        public async Task CheckAndSend(MovieDBContext dBContext)
        {
            _repository = new BaseRepository(dBContext);

            System.Collections.Generic.List<MessageQueue> messageQueues = await dBContext.MessageQueues
                .OrderBy(mq => mq.Date)
                .ToListAsync();

            foreach (MessageQueue messageQueue in messageQueues)
            {
                if (messageQueue.Type == MessageType.Email.ToString())
                {
                    await SendEmailAsync(messageQueue);
                }
                else if (messageQueue.Type == MessageType.Phone.ToString())
                {
                    await SendPhoneAsync(messageQueue);
                }

                await _repository.RemoveAsync<MessageQueue>(messageQueue);
                await _repository.AddAsync<MessageLog>(new MessageLog()
                {
                    Id = messageQueue.Id,
                    Type = messageQueue.Type,
                    ContactAddress = messageQueue.ContactAddress,
                    Subject = messageQueue.Subject,
                    Body = messageQueue.Body,
                    Status = MessageStatus.Sent.ToString(),
                    SendDate = DateTime.UtcNow
                });
            }
        }

        private async Task SendEmailAsync(MessageQueue messageQueue)
        {
            ServerOption smtpAddressOption = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "movie.email.smtp.address");
            string smtpAddress = smtpAddressOption.Value;

            ServerOption portNumberOption = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "movie.email.port.number");
            int portNumber = int.Parse(portNumberOption.Value);

            ServerOption enableSSLOption = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "movie.email.enamble.ssl");
            bool enableSSL = bool.Parse(enableSSLOption.Value);

            ServerOption emailFromAddressOption = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "movie.email.address");
            string emailFromAddress = emailFromAddressOption.Value;

            ServerOption passwordOption = await _repository
                .FirstOrDefaultAsync<ServerOption>(op => op.Key == "movie.email.address.password");
            string password = passwordOption.Value;

            using MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(messageQueue.ContactAddress);
            mail.Subject = messageQueue.Subject;
            mail.Body = messageQueue.Body;
            mail.IsBodyHtml = true;
            using SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            smtp.EnableSsl = enableSSL;
            await smtp.SendMailAsync(mail);
        }

        private Task SendPhoneAsync(MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }
    }
}
