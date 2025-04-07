using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppBlog.Data;
using WebAppBlog.Models;
using WebAppBlog.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var ConnectionString = builder.Configuration.GetConnectionString("CadenaSQL");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

// WebAppBlog>Models>ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

DataSeeding();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitialize = app.Services.GetRequiredService<IDbInitializer>();
        dbInitialize.Initialize();
    }
}