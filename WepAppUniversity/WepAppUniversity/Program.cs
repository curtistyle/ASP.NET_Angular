using Microsoft.EntityFrameworkCore;
using WebAppUniversity.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CadenaSQL") ?? throw new InvalidOperationException("Connection string 'UniversityDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UniversityDbContext>(options =>
{
	options.UseSqlServer(
		builder.Configuration
		.GetConnectionString("CadenaSQL")
		);
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
	{ 
		options.SignIn.RequireConfirmedAccount = false;
		options.Password.RequireDigit = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 8;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<UniversityDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

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

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var roleManager =
		scope.ServiceProvider
		.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new[] { "admin", "professor", "student" };
	foreach (var role in roles)
	{
		if(!await roleManager.RoleExistsAsync(role))
			await roleManager.CreateAsync(new IdentityRole(role));
	}
}

using (var scope = app.Services.CreateScope())
{
	var userManager = 
		scope.ServiceProvider
		.GetService<UserManager<IdentityUser>>();

	string email = "pepeto@admin.com";
	string password = "admin1234";

	if (await userManager.FindByEmailAsync(email) == null)
	{
		var userAdmin = new IdentityUser();
		userAdmin.UserName = "Pepeto";
		userAdmin.Email = email;
		await userManager.CreateAsync(userAdmin, password);
		await userManager.AddToRoleAsync(userAdmin, "admin");
	}
}

app.Run();
