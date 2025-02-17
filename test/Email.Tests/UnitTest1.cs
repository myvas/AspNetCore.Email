using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Myvas.AspNetCore.Email;
using IEmailSender = Myvas.AspNetCore.Email.IEmailSender;

namespace Email.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task SendEmail_Test()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddEmail(options =>
            {
                options.SmtpServerAddress = "smtp.myvas.com";
                options.SenderAccount = "test";
                options.SenderPassword = "WrongPassword@1";
                options.SenderDisplayName = "UnitTest";
            });
            var app = builder.Build();

            IEmailSender _emailSender = app.Services.GetRequiredService<IEmailSender>();

            var emailDto = new EmailDto()
            {
                Recipients = "4848285@qq.com;noreply@myvas.com",
                Subject = "Test Myvas.AspNetCore.Email (Plain Text)",
                Body = @"Hello, 

This is a TEST email from Myvas.AspNetCore.Email.Tests.

Best Regards,
Myvas.AspNetCore.Email",
            };
            var result = await _emailSender.SendEmailAsync(emailDto);
            Assert.False(result);
        }
    }
}