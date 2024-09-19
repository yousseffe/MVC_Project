using MVC_3.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male = 1,
        [EnumMember(Value ="Female")]
        Female = 2
    }
    public enum EmployeeType
    {
        FullTime = 1,
        PartTime = 2
    }
    public class Employee : ModelBase
    {
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50 , ErrorMessage ="Max Length for name is 50")]
        [MinLength(4 , ErrorMessage ="Min Length For name is 4")]
        public string Name { get; set; }
        [Range(21, 60)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{3,20}-[a-zA-Z]{3,20}-[a-zA-Z]{3,20}$", ErrorMessage = "Address must be in the format 123-street-city-country.")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; }

        public Gender Gender { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string ImageName {  get; set; }
    }
}
