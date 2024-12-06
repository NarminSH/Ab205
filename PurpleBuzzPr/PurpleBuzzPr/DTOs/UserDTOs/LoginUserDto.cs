using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzPr.DTOs.UserDTOs
{
    public class LoginUserDto
    {
        [Required]
        [Display(Prompt = "Email or Username")]
        public string EmailOrUsername { get; set; }

        [DataType(DataType.Password), Required]
        [Display(Prompt = "Password")]
        public string Password { get; set; }
        [Required]
        public bool IsPersistant { get; set; }
    }
}
