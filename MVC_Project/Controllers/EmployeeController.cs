using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_3.DAL.Models;
using MVC_Project.BLL.Interface;
using MVC_Project.DAL.Models;
using MVC_Project.ViewModels;
using System;
using System.Collections.Generic;

namespace MVC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env ,IMapper mapper)
        {
            _repository = employeeRepository;
            _env = env;
            _mapper = mapper;
        }
        public IActionResult Index(string SearchInput)
        {
            ViewBag.Message = "All Employees";
            if (string.IsNullOrWhiteSpace(SearchInput))
            {
                var employees = _repository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmp);
            }
            else
            {
                var employees = _repository.GetEmployeesByName(SearchInput);
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
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employee);
                var count = _repository.Add(mappedEmp);
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
            var employee = _repository.GetByID(id.Value);
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
                var count = _repository.Update(mappedEmp);
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
                _repository.Delete(mappedEmp);
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
