
# Myvas.AspNetCore.Email

## What is this?

An AspNetCore service to send Email via MailKit. (Windows and Linux works!)

## How to Use
### Nuget
https://www.nuget.org/packages/Myvas.AspNetCore.Email

### Web App:
```csharp
services.AddEmail(options =>
{
    options.SmtpServerAddress = Configuration["Email:SmtpServerAddress"];
    options.SenderAccount = Configuration["Email:SenderAccount"];
    options.SenderPassword = Configuration["Email:SenderPassword"];
    options.SenderDisplayName = Configuration["Email:SenderDisplayName"];
});
```

### Web Api:

POST api/v1/Email
```
{
  "recipients": "4848285@qq.com;noreply@test.com",
  "subject": "来自WebApi的测试邮件",  
  "body": "这是一封来自WebApi的测试邮件，您无须理会此邮件。"
}
```

## How to Build

Visual Studio 2017 and .NET Core SDK 2.1

Download from Microsoft's official website: http://asp.net
