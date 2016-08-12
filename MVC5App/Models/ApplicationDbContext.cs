using Microsoft.AspNet.Identity.EntityFramework;

namespace MVC5App.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(Helpers.GetRDSConnection() ?? "DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

    }
}