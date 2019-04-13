using System.Collections.Generic;

namespace Myvas.AspNetCore.Email
{
    public interface IEmailTemplate
    {
        string GetContent(string templateRelativePath, params string[] data);
        string GetContent(string templateRelativePath, Dictionary<string, string> data);
    }
}