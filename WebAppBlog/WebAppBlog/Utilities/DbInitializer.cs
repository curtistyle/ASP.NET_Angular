using Microsoft.AspNetCore.Identity;
using WebAppBlog.Data;
using WebAppBlog.Models;

namespace WebAppBlog.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleMan)
        {
            _context = context;
            _userManager = userManager; 
            _roleManager = roleMan; 
        }
        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(WebSitesRules.WebsiteAdmin).GetAwaiter().GetResult()){
                _roleManager.CreateAsync(new IdentityRole(WebSitesRules.WebsiteAdmin)).GetAwaiter();
                _roleManager.CreateAsync(new IdentityRole(WebSitesRules.WebsiteAuthor)).GetAwaiter();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName= "admin",
                    Email="admin@admin.com",
                    FirstName ="Super",
                    LastName ="Admin",
                }, "Admin123").GetAwaiter();
            
                var appUser = _context.ApplicationUsers!.FirstOrDefault( value => value.Email == "admin@admin.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebSitesRules.WebsiteAdmin).GetAwaiter().GetResult();
                }

                //var aboutPage = new Page()
                //{
                //    Title = "About Us",
                //    Slug = "about-Us"
                //};

                //var contactPage = new Page()
                //{
                //    Title = "Contact Us",
                //    Slug = "contact"
                //};

                //var privatePolicyPage = new Page()
                //{
                //    Title = "Privacy Policy Us",
                //    Slug = "private"
                //};

                //_context.Pages.Add(aboutPage);
                //_context.Pages.Add(contactPage);
                //_context.Pages.Add(privatePolicyPage);
                //_context.SaveChanges();

                var listOfPage = new List<Page>()
                {
                    new Page()
                    {
                        Title = "About Us",
                        Slug = "about"
                    },
                    new Page()
                    {
                        Title = "Contact Us",
                        Slug = "contact"
                    },
                    new Page()
                    {
                        Title = "Privacy Policy Us",
                        Slug = "private"
                    }
                };

                _context.Pages.AddRange(listOfPage);
                _context.SaveChanges();
            }
        }
    }
}

