using System.ComponentModel.DataAnnotations;

namespace BookHiveApi.Models.Dtos
{
    public class ResponseAuth
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
