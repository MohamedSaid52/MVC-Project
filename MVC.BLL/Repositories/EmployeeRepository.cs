using MVC.BLL.Interfaces;
using MVC.DAL.Contexts;
using MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Repositories
{
    public class EmployeeRepository : GenricRepository<Employee>,IEmployeeRepository
    {
        private readonly MvcAppDbContext _context;
        public EmployeeRepository(MvcAppDbContext context):base(context)
        {
            this._context = context;
        }

        public IEnumerable<Employee> Search(string Name)
        {
           return _context.Employees.Where(e=>e.Name.Contains(Name)).ToList();
        }
        //public int Add(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //=> _context.Employees.ToList();

        //public Employee GetById(int id)
        //=> _context.Employees.FirstOrDefault(x => x.Id == id);

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}
    }
}
