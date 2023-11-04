using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "In Valid Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length Is 6 Chars")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
