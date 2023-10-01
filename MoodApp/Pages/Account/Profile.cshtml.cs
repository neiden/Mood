using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using MoodApp.Models;


namespace MoodApp.Pages;

public class ProfileModel : PageModel
{
    private readonly MoodApp.Data.MoodContext _context;

    public ProfileModel(MoodApp.Data.MoodContext context)
    {
        _context = context;
    }

    public string UserName { get; set; }
    public string UserEmailAddress { get; set; }
    public string UserProfileImage { get; set; }
    public User CurrUser { get; set; } = default!;

    public async Task OnGetAsync()
    {
        UserName = User.Identity.Name;
        UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        UserProfileImage = User.FindFirst(c => c.Type == "picture")?.Value;
        // if (UserEmailAddress == null || _context.Users == null)
        // {
        //     return NotFound();
        // }

        var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == UserEmailAddress);
        CurrUser = user;
    }
}