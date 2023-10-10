using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MoodApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace MoodApp.Services;

public class PostService : IPostService
{
    private readonly MoodApp.Data.MoodContext _context;

    public PostService(MoodApp.Data.MoodContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<List<Post>> GetPostsAsync()
    {
        var Posts = await _context.Posts.ToListAsync();
        return Posts;
    }
    [HttpGet]
    public async Task<Post> GetPostAsync(int id)
    {
        var Post = await _context.Posts.FirstOrDefaultAsync(m => m.ID == id);
        return Post;
    }

    [HttpGet]
    public async Task<List<Post>> GetUserPostsAsync(int uID)
    {
        return await Task.FromResult(_context.Posts.Where(m => m.UserID == uID).ToList());
    }

    //Consider changing this to return a response code 
    // Task<IActionResult> with 404 if failed and 200 if success
    [HttpPost]
    public async Task CreatePostAsync(Post post)     
    {
        Log.Information("Attempting to create this post: ID: " + post.ID + " userID: " + post.UserID + " content:" + post.Content + " date:" + post.PostDate);
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

    }
}
