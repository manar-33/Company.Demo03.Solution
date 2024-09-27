using AutoMapper;
using Company.Demo03.BLL.Interfaces;
using Company.Demo03.DAL.Models;
using Company.Demo03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


namespace Company.Demo03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            IUnitOfWork unitOfWork,
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IMapper mapper
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string InputSearch)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetByName(InputSearch);
            }

           var result= _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            //ViewData["Data01"] = "Hello From ViewData";
            //ViewBag.Data02 = "Hello From ViewBag";
            //TempData["Data03"] = "Hello From TempData";
            return View(result);
            
        }
        public IActionResult Create()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            //Employee employee = new Employee()
            //{
            //    Id = model.Id,
            //    Address = model.Address,
            //    Name = model.Name,
            //    Salary = model.Salary,
            //    Age = model.Age,
            //    HiringDate = model.HiringDate,
            //    Email = model.Email,
            //    IsActive = model.IsActive,
            //    WorkFor = model.WorkFor,
            //    WorkForId = model.WorkForId,
            //    PhoneNumber = model.PhoneNumber
            //};
            
            if (ModelState.IsValid)
            {
                try
                {

                     _unitOfWork.EmployeeRepository.Add(model);
                    var Count = _unitOfWork.Complete();
                    if (Count > 0)
                    {
                        TempData["Message"] = "Employee Created";
                    }
                    else
                    {
                        TempData["Message"] = "Employee Not Created";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound();
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Address = employee.Address,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HiringDate = employee.HiringDate,
            //    Email = employee.Email,
            //    IsActive = employee.IsActive,
            //    WorkFor = employee.WorkFor,
            //    WorkForId = employee.WorkForId,
            //    PhoneNumber = employee.PhoneNumber
            //};
            return View(viewName, employee);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);


            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee model)
        {

            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                     _unitOfWork.EmployeeRepository.Update(model);
                    var Count = _unitOfWork.Complete();
                    if (Count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                     _unitOfWork.EmployeeRepository.Delete(model);
                    var Count = _unitOfWork.Complete();
                    if (Count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }
    }
}
