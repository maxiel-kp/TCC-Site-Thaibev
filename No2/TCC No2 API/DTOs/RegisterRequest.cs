using System.ComponentModel.DataAnnotations;

namespace TCC_No2_API.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(8)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
