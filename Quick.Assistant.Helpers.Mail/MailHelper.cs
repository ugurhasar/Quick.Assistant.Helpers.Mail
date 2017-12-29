using System;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Assistant.Helpers.Mail
{
  public class MailHelper
  {
    public MailHelper(SmtpCredentials smtpCredentials)
    {
      this.Initialize(smtpCredentials);
    }

    private void Initialize(SmtpCredentials smtpCredentials)
    {
      if (smtpCredentials == null)
        throw new Exception("SmtpCredentials object must not null!");

      if (smtpCredentials.SmtpSection == null)
        throw new Exception("SmtpSection object must not null!");

      this.smtpClient = new SmtpClient();
      smtpClient.Host = smtpCredentials.SmtpSection.Network.Host;
      smtpClient.Port = smtpCredentials.SmtpSection.Network.Port;
      smtpClient.Credentials = new NetworkCredential(smtpCredentials.SmtpSection.Network.UserName, smtpCredentials.SmtpSection.Network.Password);
      //smtpClient.UseDefaultCredentials = false;
      //smtpClient.DeliveryMethod = SmtpDeliveryMethod..Network;
      //smtpClient.EnableSsl = true;

      this.mailMessage = new MailMessage();
      this.mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
      this.mailMessage.BodyEncoding = Encoding.UTF8;
      this.mailMessage.IsBodyHtml = true;
      this.mailMessage.Attachments.Clear();
      this.mailMessage.To.Clear();
      this.mailMessage.Bcc.Clear();
      this.mailMessage.Sender = this.mailMessage.From = new MailAddress(
          smtpCredentials.SmtpSection.Network.UserName, 
          string.IsNullOrEmpty(smtpCredentials.DisplayName) ? smtpCredentials.SmtpSection.Network.UserName : smtpCredentials.DisplayName
        );

      this.mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
      this.mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
    }

    private SmtpClient smtpClient = null;
    private MailMessage mailMessage = null;

    public void Send(string body, string subject, string to)
    {
      this.Send(body, subject, new string[] { to }, new string[0], new string[0], new Attachment[0]);
    }

    public void Send(string body, string subject, string[] to)
    {
      this.Send(body, subject, to, new string[0], new string[0], new Attachment[0]);
    }

    public void Send(string body, string subject, string to, Attachment attachment)
    {
      this.Send(body, subject, new string[] { to }, new string[0], new string[0], new Attachment[] { attachment });
    }

    public void Send(string body, string subject, string[] tos, string[] ccs, string[] bccs, Attachment[] attachments)
    {
      try
      {
        this.mailMessage.Attachments.Clear();
        this.mailMessage.To.Clear();
        this.mailMessage.Bcc.Clear();
        this.mailMessage.CC.Clear();

        this.mailMessage.Body = body;
        this.mailMessage.Subject = subject;

        foreach (string to in tos)
          this.mailMessage.To.Add(to);

        foreach (string cc in ccs)
          this.mailMessage.To.Add(cc);

        foreach (string bcc in bccs)
          this.mailMessage.Bcc.Add(bcc);

        foreach (Attachment attachment in attachments)
          this.mailMessage.Attachments.Add(attachment);

        this.smtpClient.Send(this.mailMessage);
      }
      catch (Exception exception)
      {
        throw exception;
      }
    }

    public void Dispose()
    {
      this.mailMessage.Dispose();
      this.mailMessage = null;
      this.smtpClient = null;
    }
  }
}
