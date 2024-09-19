using MVC_3.DAL.Models;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.BLL.Interface
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
