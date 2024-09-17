using Microsoft.EntityFrameworkCore;
using MVC_3.DAL.Data.Contexts;
using MVC_3.DAL.Models;
using MVC_Project.BLL.Interface;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Repositories
{
    public class EmployeeRepository :  GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) { }


        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _DbContext.Employees.Where(e => e.Address.ToLower().Contains(address.ToLower()));
        }

        public IQueryable<Employee> GetEmployeesByName(string name)
        {
            return _DbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
