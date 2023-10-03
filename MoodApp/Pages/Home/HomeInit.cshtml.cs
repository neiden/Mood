using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace MoodApp.Pages
{
    public class HomeInitModel : PageModel
    {

        private readonly MoodApp.Data.MoodContext _context;

        public HomeInitModel(MoodApp.Data.MoodContext context)
        {
            _context = context;
        }

        public string UserEmailAddress { get; set; }
        public async Task OnGetAsync()
        {
            UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == UserEmailAddress);
            if (user == null)
            {
                Response.Redirect("/Account/Create");
            }
            else
            {
                Response.Cookies.Append("UID", user.ID + "");
                Response.Redirect("/Home/Home");
            }
        }
    }
}
