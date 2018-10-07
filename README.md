
# AspNetCore.Email

[![Travis build status](https://img.shields.io/travis/huangxiangyao/AspNetCore.EmailMiddleware.svg?label=travis-ci&style=flat-square&branch=master)](https://travis-ci.org/huangxiangyao/AspNetCore.EmailMiddleware)
[![AppVeyor build status](https://img.shields.io/appveyor/ci/FrankH/AspNetCore-EmailMiddleware/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/FrankH/AspNetCore-EmailMiddleware)

## What is this?

An AspNetCore service to send Email. (It works on Windows, and Linux!)

## How to Use
### Web App:
```csharp
app.UseEmail(options=>{...});
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

How to Build

Use Visual Studio 2017 v15.8.2+ and .NET Core SDK v2.1.403+ installed.

Download from Microsoft's official website: http://asp.net
