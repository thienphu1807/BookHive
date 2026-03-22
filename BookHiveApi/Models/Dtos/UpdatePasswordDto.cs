using System.ComponentModel.DataAnnotations;

namespace BookHiveApi.Models.Dtos
{
    public class UpdatePasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
