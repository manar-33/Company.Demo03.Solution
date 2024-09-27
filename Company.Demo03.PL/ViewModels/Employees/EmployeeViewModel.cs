using Company.Demo03.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.Demo03.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
        public string Name { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[A-Za-z]{5,10}-[A-Za-z]{5,10}-[A-Za-z]{5,10}$", ErrorMessage = "Address Must Be Like This 123-Street-City-Country")]
        public string Address { get; set; }
        [Range(25, 60, ErrorMessage = "Age Must Be Between 25 to 60")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Salary Is Required!")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        //[EmailAddress(ErrorMessage ="Email is not valid")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        //[DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-9]{8}$")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; } //Navigational Property
    }
}
