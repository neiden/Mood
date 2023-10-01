
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;

namespace MoodApp.Pages;

public class LoginModel : PageModel
{
    public async Task OnGet(string returnUrl = "./Home/HomeInit")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                    .WithRedirectUri(returnUrl)
                    .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }
}