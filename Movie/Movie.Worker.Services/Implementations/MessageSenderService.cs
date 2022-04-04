using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Domain.POCO;
using Movie.Persistance.Context;
using Movie.Services.Abstractions;
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
        public async Task CheckAndSend(MovieDBContext dBContext)
        {
            var messageQueues = await dBContext.MessageQueues
                .OrderBy(mq => mq.Date)
                .ToListAsync();

            foreach (var messageQueue in messageQueues)
            {
                if (messageQueue.Type == MessageType.Email.ToString())
                    await SendEmailAsync(messageQueue, dBContext);
                else if (messageQueue.Type == MessageType.Phone.ToString())
                    await SendPhoneAsync(messageQueue, dBContext);

                dBContext.MessageQueues.Remove(messageQueue);
                await dBContext.SaveChangesAsync();
                await dBContext.MessageLogs.AddAsync(new MessageLog()
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

        private async Task SendEmailAsync(MessageQueue messageQueue, MovieDBContext dBContext)
        {
            var smtpAddressOption = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "movie.email.smtp.address");
            string smtpAddress = smtpAddressOption.Value;
            var portNumberOption = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "movie.email.port.number");
            int portNumber = int.Parse(portNumberOption.Value);
            var enableSSLOption = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "movie.email.enamble.ssl");
            bool enableSSL = bool.Parse(enableSSLOption.Value);
            var emailFromAddressOption = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "movie.email.address");
            string emailFromAddress = emailFromAddressOption.Value;
            var passwordOption = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "movie.email.address.password");
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

        private Task SendPhoneAsync(MessageQueue messageQueue, MovieDBContext dBContext)
        {
            throw new NotImplementedException();
        }
    }
}
