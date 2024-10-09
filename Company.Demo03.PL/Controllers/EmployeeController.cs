using AutoMapper;
using Company.Demo03.BLL.Interfaces;
using Company.Demo03.BLL.Repository;
using Company.Demo03.DAL.Models;
using Company.Demo03.PL.Helper;
using Company.Demo03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;



namespace Company.Demo03.PL.Controllers
{
	[Authorize]
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

        public async Task<IActionResult> Index(string InputSearch)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees =await _unitOfWork.EmployeeRepository.GetByNameAsync(InputSearch);
            }

           var result= _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            //ViewData["Data01"] = "Hello From ViewData";
            //ViewBag.Data02 = "Hello From ViewBag";
            //TempData["Data03"] = "Hello From TempData";
            return View(result);
            
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
          
           
            if (ModelState.IsValid)
            {
                try
                {
                   if(model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.Upload(model.Image, "images"); 
                    }
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
                    var employee=_mapper.Map<Employee>(model);
                   await _unitOfWork.EmployeeRepository.AddAsync(employee);
                    var Count =await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
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
          var employeeViewModel =  _mapper.Map<EmployeeViewModel>(employee);
            return View(viewName, employeeViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            try
            {

                var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewData["departments"] = departments;
                if (id is null) return BadRequest();
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
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
               var employeeViewModel= _mapper.Map<EmployeeViewModel>(employee);
                return View(employeeViewModel);
            }
            catch (Exception ex) 
            {
            ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Home","Error");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {

            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.Delete(model.ImageName, "images");
                    }
                    if (model.Image is not null)
                    {
                       model.ImageName= DocumentSettings.Upload(model.Image, "images");
                    }
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
                    var employee = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRepository.Update(employee);
                    var Count = await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            try
            {
                if (id is null) return BadRequest();
                var employee =await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
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
                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
                return View(employeeViewModel);
            }
            catch (Exception ex) { 
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error","Home");
            }
            //return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
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
                    var employee = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRepository.Delete(employee);
                    var Count = await _unitOfWork.CompleteAsync();
                    if (Count > 0)
                    {
                        if (model.ImageName is not null)
                        {
                            DocumentSettings.Delete(model.ImageName, "images");
                        }
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
