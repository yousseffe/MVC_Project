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
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly AppDbContext _DbContext;
        public DepartmentRepository(AppDbContext DbContext) : base(DbContext) {}

    }
}
