using Microsoft.AspNetCore.Identity.UI.Services;

namespace RecipeWebsiteMVC.Models.EmailSender
{
    /// <summary>
    /// Fake Email Sender to Identity framework.
    /// 
    /// </summary>
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
