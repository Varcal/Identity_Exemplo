using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}