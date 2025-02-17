using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.EmailApi.WebClient
{
    public static class Program
    {
        static Program()
        {
            // Console output in Chinese
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static void Main() => RunAsync().Wait();

        static readonly HttpClient client = new();

        static async Task RunAsync()
        {
            var baseUri = new Uri("http://localhost:9002/");
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var timestamp = DateTime.Now.Ticks;
            var json = JsonConvert.SerializeObject(new
            {
                Recipients = "4848285@qq.com",
                Subject = $"来自WebApi的测试邮件({timestamp})",
                Body = $"这是一封来自WebApi的测试邮件，编号：{timestamp}。您无须理会此邮件。如有打扰请见谅。"
            });
            var content = new StringContent(
                json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Email", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString == "false")
            {
                Console.WriteLine("The email api method called, but your email probably failed to sent out!");
            }
            else
            {
                Console.WriteLine("The email api method had invoked, and your email should be sent out successfully!");
            }
            Console.ReadLine();
        }
    }
}
