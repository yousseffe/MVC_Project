using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_3.DAL.Models;
using MVC_Project.BLL.Interface;
using MVC_Project.DAL.Models;
using MVC_Project.Helpers;
using MVC_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MVC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }
        public IActionResult Index(string SearchInput)
        {
            ViewBag.Message = "All Employees";
            if (string.IsNullOrWhiteSpace(SearchInput))
            {
                var employees = _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmp);
            }
            else
            {
                var employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(SearchInput);
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmp);
            }
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageName =  DocumentSettings.UploadFile(employee.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
				_unitOfWork.EmployeeRepository.Add(mappedEmp);
                var count = _unitOfWork.Complete();
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
                return BadRequest();
            }
            var employee = _unitOfWork.EmployeeRepository.GetByID(id.Value);
            return View(viewName, employee);
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int? id)
        {
            return Details(id , "Edit");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(employee);
            }


            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
				_unitOfWork.EmployeeRepository.Update(mappedEmp);
                var count = _unitOfWork.Complete();
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
        public IActionResult Delete(EmployeeViewModel employee)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
				_unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
                DocumentSettings.DeleteFile(employee.ImageName, "Images");
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
