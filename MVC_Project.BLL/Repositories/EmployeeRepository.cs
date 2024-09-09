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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _DbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _DbContext = appDbContext;
        }
        public int Add(Employee employee)
        {
            _DbContext.Employees.Add(employee);
            return _DbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _DbContext.Employees.Remove(employee);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _DbContext.Employees.AsNoTracking().ToList();
        }

        public Employee GetByID(int id)
        {
            return _DbContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            _DbContext.Employees.Update(employee);
            return _DbContext.SaveChanges();
        }
    }
}
