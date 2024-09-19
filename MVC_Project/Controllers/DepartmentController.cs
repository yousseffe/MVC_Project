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

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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
				_unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
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
            var Department = _unitOfWork.DepartmentRepository.GetByID(id.Value);
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
            var department = _unitOfWork.DepartmentRepository.GetByID(id.Value);

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
				_unitOfWork.DepartmentRepository.Update(department);
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
				_unitOfWork.DepartmentRepository.Delete(department);
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

