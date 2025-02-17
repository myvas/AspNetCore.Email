using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Email.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddEmail(options =>
            {
                options.SmtpServerAddress = "smtp.myvas.com";
                options.SenderAccount = "test";
                options.SenderPassword = "Password@1";
                options.SenderDisplayName = "UnitTest";
            });
        }
    }
}