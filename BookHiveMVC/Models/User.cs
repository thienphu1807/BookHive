using Microsoft.AspNetCore.Identity;

namespace BookHiveMVC.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserBookReview> UserBookReviews { get; set; }
    }
}
