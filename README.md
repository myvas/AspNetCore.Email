
# Myvas.AspNetCore.Email

## What is this?

An AspNetCore service to send Email via MailKit. (Windows and Linux works!)

## How to Use
### Nuget
https://www.nuget.org/packages/Myvas.AspNetCore.Email

### secrets.json or appsettings.xxx.json
```
  "Email:SmtpServerSsl": "true",
  "Email:SmtpServerPort": "465",
  "Email:SmtpServerAddress": "smtp.myvas.com",
  "Email:SenderPassword": "yY_myvas.com_idc",
  "Email:SenderDisplayName": "DO-NOT-REPLY",
  "Email:SenderAccount": "noreply@myvas.com",
```
### ConfigureServices:
```csharp
services.AddEmail(options =>
{
    options.SmtpServerAddress = Configuration["Email:SmtpServerAddress"];
    options.SenderAccount = Configuration["Email:SenderAccount"];
    options.SenderPassword = Configuration["Email:SenderPassword"];
    options.SenderDisplayName = Configuration["Email:SenderDisplayName"];
});
```

### Use Case 1: Implementation of Microsoft.AspNetCore.Identity.UI.Services.IEmailSender:
```csharp
public class EmailService : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
{
    private readonly EmailSender _emailSender;

    public EmailService(EmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return _emailSender.SendEmailAsync(email, subject, htmlMessage);
    }
}
```

### Use Case 2: Use Myvas.AspNetCore.Email.IEmailSender without 'Microsoft.AspNetCore.Identity.UI'
```
using Myvas.AspNetCore.Email;

public class EmailController : Controller
{
    private readonly IEmailSender _emailSender;

    public EmailController(
        IEmailSender emailSender)
    {
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
    }
```

### WebApiDemo
POST api/v1/Email
```
{
  "recipients": "4848285@qq.com;noreply@test.com",
  "subject": "来自WebApi的测试邮件",  
  "body": "这是一封来自WebApi的测试邮件，您无须理会此邮件。"
}
```

## Next...
* Email templates
* Razor Class Library for Configuration and Management
