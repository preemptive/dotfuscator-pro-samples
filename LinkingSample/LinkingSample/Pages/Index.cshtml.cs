using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Authenticator;

namespace LinkingSample.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Username = "";
            Password = "";
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostSubmitForm(string username, string password)
        {
            if (Authenticate.AuthenticateLogin(username, password))
                return RedirectToPage("Dashboard");
            else
                return RedirectToPage("Error");
        }
    }
}