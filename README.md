
AspNetCore.EmailMiddleware
=====================

[![Travis build status](https://img.shields.io/travis/huangxiangyao/AspNetCore.EmailMiddleware.svg?label=travis-ci&style=flat-square&branch=master)](https://travis-ci.org/huangxiangyao/AspNetCore.EmailMiddleware)
[![AppVeyor build status](https://img.shields.io/appveyor/ci/FrankH/AspNetCore-EmailMiddleware/master.svg?label=appveyor&style=flat-square)](https://ci.appveyor.com/project/FrankH/AspNetCore-EmailMiddleware)

What is this?
----------------

A Middleware to Send Email built on latest AspNetCore (Tested on Linux or Windows)

How to Use
----------------
(1) For Another AspNetCore Project:

app.UseEmail(options=>{...});

or (2) For A Web Client Project:

POST api/v1/email/send
{
  "subject": "来自WebApi的测试邮件",
  "body": "这是一封来自WebApi的测试邮件，您无须理会此邮件。"
}

How to Build
----------------

Use Visual Studio 2015 with Update 3 and .NET Core 1.1 installed.
Download from Microsoft's official website: http://asp.net
