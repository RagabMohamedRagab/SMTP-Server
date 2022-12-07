using SendEmail.ViewsModel;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.IO;


namespace SendEmail.Utitles {
    public class SendToEmail : ISendToEmail {
        private readonly MailSettings _mailSettings;

        public SendToEmail(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendMessage(FromToVM model)
        {
            try
            {
                var email = new MimeMessage()
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Email),
                    Subject = model.Subject ?? string.Empty,
                };
                email.To.Add(MailboxAddress.Parse(model.To));
                var builder = new BodyBuilder();
                if (model.Attachments != null)
                {
                    byte[] FileBytes;
                    foreach (var file in model.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using var ms = new MemoryStream();
                            file.CopyTo(ms);
                            FileBytes = ms.ToArray();
                            builder.Attachments.Add(file.FileName, FileBytes, ContentType.Parse(file.ContentType));
                        }

                    }
                }
                builder.TextBody = model.Body;
                email.Body = builder.ToMessageBody();
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
           
           
        }

      
    }
}
