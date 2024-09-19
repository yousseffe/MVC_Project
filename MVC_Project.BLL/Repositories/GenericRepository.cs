using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext _DbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _DbContext = appDbContext;
        }
        public void Add(T item)
        {
            _DbContext.Add(item);
             _DbContext.SaveChanges();
        }
        public void Delete(T item)
        {
            _DbContext.Remove(item);
             _DbContext.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return _DbContext.Set<T>().AsNoTracking().ToList();
        }
        public T GetByID(int id)
        {
            return _DbContext.Set<T>().Find(id);
        }
        public void Update(T item)
        {
            _DbContext.Set<T>().Update(item);
             _DbContext.SaveChanges();
        }
    }
}
