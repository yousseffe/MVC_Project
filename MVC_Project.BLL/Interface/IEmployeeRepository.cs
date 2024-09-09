using MVC_3.DAL.Models;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetByID(int id);
        int Add(Employee department);
        int Update(Employee department);
        int Delete(Employee department);
    }
}
