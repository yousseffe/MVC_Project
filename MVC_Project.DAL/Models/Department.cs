using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3.DAL.Models
{
	public class Department : ModelBase
	{
		[Required(ErrorMessage = "Code is required!")]
		public string Code { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
		[Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

		public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
