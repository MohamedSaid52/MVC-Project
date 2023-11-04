using MVC.BLL.Interfaces;
using MVC.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Repositories
{
    public class GenricRepository<T> : IGenricRepository<T> where T : class
    {
        private readonly MvcAppDbContext context;

        public GenricRepository(MvcAppDbContext context)
        {
            this.context = context;
        }

        public int Add(T obj)
        {
            context.Set<T>().Add(obj);
            return context.SaveChanges();
        }

        public int Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            return context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        => context.Set<T>().ToList();

        public T GetById(int id)
        => context.Set<T>().Find(id);

        public int Update(T obj)
        {
            context.Set<T>().Update(obj);
            return context.SaveChanges();
        }
    }
}
