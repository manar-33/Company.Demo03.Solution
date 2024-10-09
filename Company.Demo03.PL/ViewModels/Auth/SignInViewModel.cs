using System.ComponentModel.DataAnnotations;

namespace Company.Demo03.PL.ViewModels.Auth
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage = "Invalid Email !!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        [MaxLength(16)]
        [MinLength(4)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
