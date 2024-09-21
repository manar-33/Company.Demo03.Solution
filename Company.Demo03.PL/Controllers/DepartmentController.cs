using Company.Demo03.BLL.Interfaces;
using Company.Demo03.BLL.Repository;
using Company.Demo03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Demo03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository= departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {

           var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {
          return View();    
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var Count = _departmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

                return View(model);
        }
        public IActionResult Details(int? id) 
        {
            if(id is null) return BadRequest();
            
            var department= _departmentRepository.Get(id.Value); 
            if (department is null) return NotFound();
            return View(department);
            
        }
    }
}
