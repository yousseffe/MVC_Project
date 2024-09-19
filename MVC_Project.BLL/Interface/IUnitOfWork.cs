﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Interface
{
	public interface IUnitOfWork
	{
		public IEmployeeRepository EmployeeRepository { get; set; }
		public IDepartmentRepository DepartmentRepository { get; set; }
		int Complete();
		public void Dispose();
	}
}
