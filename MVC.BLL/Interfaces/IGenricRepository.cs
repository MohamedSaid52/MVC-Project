using MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Interfaces
{
    public interface IGenricRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        int Add(T obj);
        int Update(T obj);
        int Delete(T obj);
    }
}
