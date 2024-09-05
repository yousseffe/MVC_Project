using Microsoft.EntityFrameworkCore;
using MVC_3.DAL.Data.Contexts;
using MVC_3.DAL.Models;
using MVC_Project.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _DbContext;
        public DepartmentRepository(AppDbContext appDbContext) 
        {
            _DbContext = appDbContext;
        }
        public int Add(Department department)
        {
            _DbContext.Departments.Add(department);
            return _DbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _DbContext.Departments.Remove(department);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _DbContext.Departments.AsNoTracking().ToList();
        }

        public Department GetByID(int id)
        {
            return _DbContext.Departments.Find(id);
        }

        public int Update(Department department)
        {
            _DbContext.Departments.Update(department);
            return _DbContext.SaveChanges();
        }
    }
}
