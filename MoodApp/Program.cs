using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoodApp.Data;
using Auth0.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MoodContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MoodContextSQLite") ?? throw new InvalidOperationException("Connection string 'MoodContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.Scope = "openid profile email";
    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Users");
    options.Conventions.AuthorizePage("/Account/Logout");
    options.Conventions.AuthorizePage("/Account/Profile");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<MoodContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.Run();
