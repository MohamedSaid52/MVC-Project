using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class RestPasswordViewModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length Is 6 Chars")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password MisMatches")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
