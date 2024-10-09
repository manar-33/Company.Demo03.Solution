using System.ComponentModel.DataAnnotations;

namespace Company.Demo03.PL.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{
        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword doesn't match the Password !!")]
        public string ConfirmPassword { get; set; }
    }
}
