﻿using MVC_3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetByID(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}
