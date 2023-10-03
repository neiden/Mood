using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodApp.Models;
using MoodApp.Pages_Users;
using Serilog;

namespace MoodApp.Pages;
public class HomeModel : PageModel
{

    private readonly MoodApp.Data.MoodContext _context;
    public Post Post { get; set; }
    public HomeModel(MoodApp.Data.MoodContext context)
    {
        _context = context;
    }
    public static _CreatePostModel createPost { get; set; }

    public IActionResult OnGet()
    {
        Log.Information("Entered Home page");
        createPost = new _CreatePostModel(_context);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        Post = new Post();
        Log.Information("Clicked submit button");
        if (!ModelState.IsValid)
        {
            Log.Information("Model state bad?");
            return Page();
        }

        var cookie = Request.Cookies["UID"];
        if (cookie != null)
        {
            Log.Information(Post.ToString());
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

