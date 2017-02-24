using System.ComponentModel.DataAnnotations;
using Identity.Resources;

namespace Identity.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(ResourceType = typeof(Texto), Name = "PhoneNumber")]
        public string Number { get; set; }
    }
}