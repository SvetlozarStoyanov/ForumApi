using System.ComponentModel.DataAnnotations;

namespace Models.DTOs.Users.Input
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
