using System.ComponentModel.DataAnnotations;

namespace MVC.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "In Valid Email")]
        public string Email { get; set; }
    }
}
