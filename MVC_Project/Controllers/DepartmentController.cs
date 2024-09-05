using Microsoft.AspNetCore.Mvc;
using MVC_Project.BLL.Interface;

namespace MVC_Project.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            return View(departments);
        }
    }
}

