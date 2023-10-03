using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodApp.Models;
using Serilog;


namespace MoodApp.Pages_Users
{
    public class _CreatePostModel : PageModel
    {
        private readonly MoodApp.Data.MoodContext _context;

        public _CreatePostModel(MoodApp.Data.MoodContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; } = default!;
        public void OnGet()
        {
            Post = new Post();
            Log.Information("Create Post Partial view opened");
        }


        public async Task<IActionResult> OnPost()
        {
            Log.Information("Clicked submit button");
            if (!ModelState.IsValid)
            {
                Log.Information("Model state bad?");
                return Page();
            }

            var cookie = Request.Cookies["UID"];
            if (cookie != null)
            {
                Post.UserID = int.Parse(cookie);
            }
            else
            {
                Log.Information("cookie parsed wrong or bad");
                return StatusCode(500);
            }


            //Look for posting animation here? 
            Post.PostDate = DateTime.Now;

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Home/Home");
        }
    }
}
