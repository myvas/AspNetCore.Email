
AspNetCore.Email
=====================

[![Travis build status](https://img.shields.io/travis/huangxiangyao/AspNetCore.EmailMiddleware.svg?label=travis-ci&style=flat-square&branch=master)](https://travis-ci.org/huangxiangyao/AspNetCore.EmailMiddleware)
[![AppVeyor build status](https://img.shields.io/appveyor/ci/FrankH/AspNetCore-EmailMiddleware/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/FrankH/AspNetCore-EmailMiddleware)

What is this?
----------------

An AspNetCore Email service and a WebApi to send Email. (It works on Windows, and Linux!)

How to Use
----------------
* For Web App:


app.UseEmail(options=>{...});


* For Web App (as A WebApi comsumer):


POST api/v1/Email


{

  "recipients": "1@test.com;2@test.com",

  "subject": "来自WebApi的测试邮件",
  
  "body": "这是一封来自WebApi的测试邮件，您无须理会此邮件。"
  
}


How to Build
----------------

Use Visual Studio 2017 and .NET Core 1.1 installed.

Download from Microsoft's official website: http://asp.net
