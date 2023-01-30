using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
