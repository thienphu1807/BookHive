using Microsoft.AspNetCore.Identity;

namespace BookHiveApi.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserBookReview> UserBookReviews { get; set; }
    }
}
