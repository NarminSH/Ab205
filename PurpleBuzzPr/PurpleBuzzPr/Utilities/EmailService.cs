using PurpleBuzzPr.Abstractions;
using System.Net;
using System.Net.Mail;

namespace PurpleBuzzPr.Utilities
{
    public class EmailService : IEmailService
    {
        IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        public void SendWelcome(string toUser)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);
            
            MailAddress from = new MailAddress("narmin.shivakhanova@code.edu.az");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);
        
            
            message.Subject = "Hello from Purple Buzz";
            message.IsBodyHtml = true;
           
            message.Body = "Welcome to our page";
            smtp.Send(message);
        }

        public void SendConfirmEmail(string toUser, string confirmUrl)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);

            MailAddress from = new MailAddress("narmin.shivakhanova@code.edu.az");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);


            message.Subject = "Confirm Email";
            message.Body = $"<a href={confirmUrl}>Click here to confirm your account</a>";
            message.IsBodyHtml = true;
            smtp.Send(message);

        }
    }
}
