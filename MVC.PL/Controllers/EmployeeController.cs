using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC.BLL.Interfaces;
using MVC.DAL.Entities;
using MVC.PL.Helper;
using MVC.PL.Models;
using NuGet.Protocol.Core.Types;
using System.Collections;


namespace MVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index(string SearchValue ="")

        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var employees = unitOfWork.EmployeeRepository.GetAll();
                var mappedemployees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedemployees);
            }
            else
            {
                var employees = unitOfWork.EmployeeRepository.Search(SearchValue);
                var mappedemployees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedemployees);
            }
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //};
        }
        public IActionResult Create()
        {
            ViewBag.Departments=unitOfWork.DepartmentRepository .GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                var employee = mapper.Map<EmployeeViewModel,Employee>(model);
                unitOfWork.EmployeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = unitOfWork.DepartmentRepository.GetAll();
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int Id, Employee employee)
        {
            if (Id != employee.Id)
                return NotFound();

            unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
