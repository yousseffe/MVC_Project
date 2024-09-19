using MVC_3.DAL.Data.Contexts;
using MVC_Project.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Repositories
{
	public class UnitOfWork : IUnitOfWork , IDisposable
	{
		private readonly AppDbContext _dbContext;
		public IEmployeeRepository EmployeeRepository { get ; set ; }
		public IDepartmentRepository DepartmentRepository { get ; set ; }
		public UnitOfWork(AppDbContext dbContext)
		{
			_dbContext = dbContext;
			EmployeeRepository = new EmployeeRepository(dbContext);
			DepartmentRepository = new DepartmentRepository(dbContext);
		}
		public int Complete()
		{
			return _dbContext.SaveChanges();
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
