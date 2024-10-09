using System.ComponentModel.DataAnnotations;

namespace Company.Demo03.PL.ViewModels.Auth
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage ="UserName Is Required !!")]
        public string UserName { get; set; }
		[Required(ErrorMessage = "FirstName Is Required !!")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "LastName Is Required !!")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email Is Required !!")]
		[EmailAddress(ErrorMessage ="Invalid Email !!")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password Is Required !!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword Is Required !!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage = "ConfirmPassword doesn't match the Password !!")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "IsAgree Is Required !!")]
		public bool IsAgree { get; set; }



    }
}
