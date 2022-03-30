using Movie.Data;
using Movie.Domain.POCO;
using Movie.Services.Abstractions;
using Movie.Services.Enums;
using Movie.Worker.Services.Abstractions;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class MessageSenderService : IMessageSenderService
    {
        public readonly IMessageQueueRepository _messageQueueRepository;
        private readonly Movie.Services.Abstractions.IServerOptionService _serverOptionService;
        private readonly IMessageLogRepository _messageLogRepository;



        public MessageSenderService(IMessageQueueRepository messageQueueRepository,
            Movie.Services.Abstractions.IServerOptionService serverOptionService,
            IMessageLogRepository messageLogRepository)
        {
            _messageQueueRepository = messageQueueRepository;
            _serverOptionService = serverOptionService;
            _messageLogRepository = messageLogRepository;
        }

        public async Task CheckAndSend()
        {
            var messageQueues = await _messageQueueRepository.GetAllASCAsync();
            foreach (var messageQueue in messageQueues)
            {
                if (messageQueue.Type == MessageType.Email.ToString())
                    await SendEmailAsync(messageQueue);
                else if (messageQueue.Type == MessageType.Phone.ToString())
                    await SendPhoneAsync(messageQueue);

                await _messageQueueRepository.Delete(messageQueue);

                await _messageLogRepository.AddAsync(new MessageLog()
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
            var smtpAddressOption = await _serverOptionService.GetOptionAsync("movie.email.smtp.address");
            string smtpAddress = smtpAddressOption.Value;
            var portNumberOption = await _serverOptionService.GetOptionAsync("movie.email.port.number");
            int portNumber = int.Parse(portNumberOption.Value);
            var enableSSLOption = await _serverOptionService.GetOptionAsync("movie.email.enamble.ssl");
            bool enableSSL = bool.Parse(enableSSLOption.Value);
            var emailFromAddressOption = await _serverOptionService.GetOptionAsync("movie.email.address");
            string emailFromAddress = emailFromAddressOption.Value;
            var passwordOption = await _serverOptionService.GetOptionAsync("movie.email.address.password");
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

        private Task SendPhoneAsync(Domain.POCO.MessageQueue messageQueue)
        {
            throw new NotImplementedException();
        }
    }
}
