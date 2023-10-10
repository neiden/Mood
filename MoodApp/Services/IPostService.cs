using Microsoft.AspNetCore.Mvc;
using MoodApp.Models;

namespace MoodApp.Services;
public interface IPostService
{
    Task<List<Post>> GetPostsAsync();
    Task<Post> GetPostAsync(int id);
    Task<List<Post>> GetUserPostsAsync(int uID);
    Task CreatePostAsync(Post post);
}