
# Myvas.AspNetCore.Email
[![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Email.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Email)
[![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.Email?label=github)](https://github.com/myvas/AspNetCore.Email)
[![GitHub Actions Status](https://github.com/myvas/AspNetCore.Email/actions/workflows/dotnet.yml/badge.svg)](https://github.com/myvas/AspNetCore.Email/actions)

An AspNetCore service to send Email via MailKit. (Windows and Linux works!)

## Samples
### EmailApi
- EmailApi (WebApi, A consumer of this middleware): http://localhost:9002/swagger/index.html

  POST api/v1/Email
  ```
  {
      "recipients": "4848285@qq.com;noreply@test.com",
      "subject": "来自WebApi的测试邮件",  
      "body": "这是一封来自WebApi的测试邮件，您无须理会此邮件。"
  }
  ```

- EmailApi Client (Console, HttpClient): `dotnet run`
- EmailApi Client (JavascriptClient): http://localhost:9006
- EmailApi Client (WebClient/Consumer WebApp): http://localhost:9008
    
### WebApp
- WebApp (Mvc, A consumer of this middleware): http://localhost:9009

  Settings: secrets.json or appsettings.xxx.json
  ```
    "Email:SmtpServerSsl": "true",
    "Email:SmtpServerPort": "465",
    "Email:SmtpServerAddress": "smtp.myvas.com",
    "Email:SenderPassword": "<your password>",
    "Email:SenderDisplayName": "DO-NOT-REPLY",
    "Email:SenderAccount": "noreply@myvas.com",
  ```

## ConfigureServices:
1.AddEmail: IEmailSender, EmailSender
```csharp
services.AddEmail(options =>
{
    options.SmtpServerAddress = Configuration["Email:SmtpServerAddress"];
    options.SenderAccount = Configuration["Email:SenderAccount"];
    options.SenderPassword = Configuration["Email:SenderPassword"];
    options.SenderDisplayName = Configuration["Email:SenderDisplayName"];
});
```

2.AddEmailTemplate: IEmailTemplate
```csharp
services.AddEmailTemplate(options =>
{
    options.EmailTemplateRootPath = Path.Combine(_env.WebRootPath, "EmailTemplates");
});
```

## Use Case 1: Use Myvas.AspNetCore.Email.IEmailSender (without 'Microsoft.AspNetCore.Identity.UI')
```csharp
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

## Use Case 2: Implementation of Microsoft.AspNetCore.Identity.UI.Services.IEmailSender:
Use Case 2 (Step 1): EmailService
```csharp
using Myvas.AspNetCore.Email;

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

Use Case 2 (Step 2): ConfigureServices
```csharp
services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailService>();
```

## Next...
* Email templates
* Razor Class Library for Configuration and Management
