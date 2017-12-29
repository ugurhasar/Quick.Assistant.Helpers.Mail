
using System.Net.Configuration;

namespace Quick.Assistant.Helpers.Mail
{
  public class SmtpCredentials
  {
    public SmtpSection SmtpSection { get; set; }
    public string DisplayName { get; set; }
  }
}
