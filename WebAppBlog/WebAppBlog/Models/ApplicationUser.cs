using Microsoft.AspNetCore.Identity;

namespace WebAppBlog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        // relacion
        public List<Post>? Posts { get; set; }
    }
}
