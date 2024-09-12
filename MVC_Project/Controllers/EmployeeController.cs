using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_3.DAL.Models;
using MVC_Project.BLL.Interface;
using MVC_Project.DAL.Models;
using System;

namespace MVC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }
        public IActionResult Index()
        {
            var employees = _repository.GetAll();
            ViewData["Message"] = "Hello ViewData";
            ViewBag.Message = "Hello View Bag";
            return View(employees);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _repository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee created successfully";
                }
                else
                {
                    TempData["Message"] = "An Error occured";
                }
                return RedirectToAction("Index");

            }
            return View();
        }
        [AutoValidateAntiforgeryToken]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id == null || !id.HasValue)
            {
                return View("Error");
            }
            var employee = _repository.GetByID(id.Value);
            return View(viewName, employee);
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee = _repository.GetByID(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(employee);
            }


            try
            {
                _repository.Update(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured during update department");
                }
                return View(employee);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _repository.Delete(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured during deleting employee");
                }
                return View(employee);
            }
        }
    }
}
