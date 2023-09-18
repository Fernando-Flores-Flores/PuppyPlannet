using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BackPuppy.Dtos
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
