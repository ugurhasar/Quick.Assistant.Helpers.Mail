# MailHelper - a basic C# Mail Helper

### Example usage

#### app.config
```xml
<configuration>  
  <system.net>
    <mailSettings>
      <smtp from="user@domain.com">
        <network host="smtp.domain.com" port="587" password="password" userName="user@domain.com" defaultCredentials="false" enableSsl="true" />
      </smtp>
    </mailSettings>
  </system.net>
</configuration>
```

#### code
```csharp
//credentials read from config file and fill
SmtpCredentials smtpCredentials = new SmtpCredentials();
smtpCredentials.SmtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
smtpCredentials.DisplayName = "Hi! There";

MailHelper mailhelper = new MailHelper(smtpCredentials);

//fill email content and infos
string[] to = { "to@domain.com" };
string[] cc = { "cc1@domain.com", "cc2@domain.com" };
string[] bcc = { "bcc1@domain.com", "bcc2@domain.com" };

Attachment[] attchments = { new Attachment("this is a test file".ToStream(), "test.txt", MediaTypeNames.Application.Octet) };

//send email
mailhelper.Send("This email is for testing", "Test Email", to, cc, bcc, attchments);
```
    
### To Do:
* Async methods

Copyright (c) 2015 Ugur Hasar 
