using System.IO;
using System.Text;

namespace Quick.Assistant.Helpers.Mail
{
    public static class Extensions
  {
    public static Stream ToStream(this string text)
    {
      return new MemoryStream(Encoding.UTF8.GetBytes(text));
    }
  }
}
