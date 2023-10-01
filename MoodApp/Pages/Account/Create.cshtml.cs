using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodApp.Models;

namespace MoodApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MoodApp.Data.MoodContext _context;

        public CreateModel(MoodApp.Data.MoodContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            //Add this newly added user's ID to cookies; might be a _context.Users.Add() method that returns a specified value

            return RedirectToPage("/Account/Profile");
        }
    }
}

