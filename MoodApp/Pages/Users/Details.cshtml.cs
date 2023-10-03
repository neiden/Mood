using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodApp.Data;
using MoodApp.Models;

namespace MoodApp.Pages_Users
{
    public class DetailsModel : PageModel
    {
        private readonly MoodApp.Data.MoodContext _context;

        public DetailsModel(MoodApp.Data.MoodContext context)
        {
            _context = context;
        }

        public User currUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                currUser = user;
            }
            return Page();
        }
    }
}
