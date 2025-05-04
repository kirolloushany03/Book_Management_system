//using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;

namespace realationshipss.security
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string tomail, string subject, string message)
        { 
            var email = new MimeMessage();
            
            email.From.Add(new MailboxAddress(
                _configuration["Email:fromName"],
                _configuration["Email:fromEmail"])
                );

            email.To.Add(MailboxAddress.Parse(tomail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = message };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(
                        _configuration["Email:Server"],
                        int.Parse(_configuration["Email:smtpport"]!),
                        MailKit.Security.SecureSocketOptions.StartTls
                    );

                await client.AuthenticateAsync(
                    _configuration["Email:smtpuser"],
                    _configuration["Email:smtppass"]
                    );

                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex) { 
                Console.WriteLine($"❌ error sending email : {ex.Message}");
            }
        }
    }
}
