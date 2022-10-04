using System.ComponentModel.DataAnnotations;

namespace TrackAll_Backend.HelperModels
{
    public class SignUpModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
    }
}
