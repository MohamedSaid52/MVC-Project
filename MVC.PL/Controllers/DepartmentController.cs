using Microsoft.AspNetCore.Mvc;
using MVC.BLL.Interfaces;
using MVC.DAL.Entities;

namespace MVC.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository repository;
        
        public DepartmentController(IDepartmentRepository repository)
        {
            this.repository = repository;
            
        }

        public IActionResult Index()
        {
            var departments = repository.GetAll();
            //if (daepartments is not null)
            //    return NotFound();
            //ViewData["Message"] = "Hello From View Data";
            //ViewBag.Message = "Hello From View Bag";
            return View(departments);
        }
        [HttpGet] //Default
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                repository.Add(department);
                //TempData["Message"] = "Department Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        [HttpGet] //Default
        public IActionResult Edit(int? Id)
        {
            if (Id is null)
                return NotFound();
            var department = repository.GetById((int)Id);
            if(department is null)
                return NotFound();
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int id,Department department)
        {
            if(id !=department.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                repository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return NotFound();
            var department = repository.GetById((int)Id);
            if (department is null)
                return NotFound();
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int Id,Department department)
        {
            if (Id !=department.Id)
                return NotFound();

            repository.Delete(department);
            return RedirectToAction(nameof(Index));
        }


    }
}
