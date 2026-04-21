using System.ComponentModel.DataAnnotations;

namespace BookHiveMVC.Models.Dtos
{
    public class ResponseAuth
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
