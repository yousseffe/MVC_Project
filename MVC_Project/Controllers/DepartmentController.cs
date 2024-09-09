using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_3.DAL.Models;
using MVC_Project.BLL.Interface;
using MVC_Project.BLL.Repositories;
using System;

namespace MVC_Project.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _repository;
        private readonly IWebHostEnvironment _env;
        public DepartmentController(IDepartmentRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            return View(departments);
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = _repository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [AutoValidateAntiforgeryToken]
        public IActionResult Details(int? id , string viewName = "Details")
        {
            if (id == null || !id.HasValue)
            {
                return View("Error");
            }
            var Department = _repository.GetByID(id.Value);
            return View(viewName , Department);
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _repository.GetByID(id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(department);
            }


            try
            {
                _repository.Update(department);
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
                return View(department);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Details(id , "Delete");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                _repository.Delete(department);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured during deleting department");
                }
                return View(department);
            }
        }
    }
}

