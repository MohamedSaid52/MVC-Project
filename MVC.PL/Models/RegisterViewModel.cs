using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="In Valid Email")]
        public string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage ="Minimum Length Is 6 Chars")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password),ErrorMessage ="Password MisMatches")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }
    }
}
