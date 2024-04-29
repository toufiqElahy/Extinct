using Microsoft.AspNetCore.Identity;

namespace interwebz.Models
{
	public class ApplicationUser : IdentityUser
	{
        public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;
    }
}
