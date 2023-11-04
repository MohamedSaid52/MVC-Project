using MVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenricRepository<Employee>
    {
        IEnumerable<Employee> Search(string Name);
    }
}
