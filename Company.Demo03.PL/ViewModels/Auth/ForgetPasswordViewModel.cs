using System.ComponentModel.DataAnnotations;

namespace Company.Demo03.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage = "Invalid Email !!")]
        public string Email { get; set; }
    }
}
