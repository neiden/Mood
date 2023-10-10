using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoodApp.Models;
using MoodApp.Pages_Users;
using Serilog;
using MoodApp.Services;
namespace MoodApp.Pages;
public class HomeModel : PageModel
{

    private readonly MoodApp.Data.MoodContext _context;
    private readonly IPostService _postService;
    private int uID;

    [BindProperty]
    public Post Post { get; set; }
    [BindProperty]
    public List<Post> Posts { get; set; } = default!;
    public HomeModel(MoodApp.Data.MoodContext context, IPostService postService)
    {
        _context = context;
        _postService = postService;
    }

    public async Task<IActionResult> OnGet()
    {
        Log.Information("Entered Home page");
        uID = int.Parse(Request.Cookies["UID"]);
        Posts = await _postService.GetUserPostsAsync(uID);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        uID = int.Parse(Request.Cookies["UID"]);
        Log.Information("Clicked submit button");
        if (!ModelState.IsValid)
        {
            Log.Information("Model state bad?");
            return Page();
        }

        Post.UserID = uID;
        //Look for posting animation here? 
        Post.PostDate = DateTime.Now;


        await _postService.CreatePostAsync(Post);

        return RedirectToPage("/Home/Home");
    }


}

