using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
