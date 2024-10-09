using Company.Demo03.BLL;
using Company.Demo03.BLL.Interfaces;
using Company.Demo03.BLL.Repository;
using Company.Demo03.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Demo03.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;



        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            // _departmentRepository = departmentRepository;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
               await _unitOfWork.DepartmentRepository.AddAsync(model);
                var Count =await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Details(int? id , string viewName= "Details")
        {
            if (id is null) return BadRequest();

            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound();
            return View(viewName,department);

        }
        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return Details(id , "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Update(model);
                    var Count =await _unitOfWork.CompleteAsync();
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
        public Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                     _unitOfWork.DepartmentRepository.Delete(model);
                    var Count =await _unitOfWork.CompleteAsync();
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
