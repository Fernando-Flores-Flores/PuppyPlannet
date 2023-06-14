using System.ComponentModel.DataAnnotations;

namespace BackPuppy.Dtos
{
    public class CredencialesDto
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

    
    }
}
