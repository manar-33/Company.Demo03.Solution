using Company.Demo03.BLL.Interfaces;
using Company.Demo03.BLL.Repository;
using Company.Demo03.DAL.Data.Contexts;
using Company.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo03.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
            _departmentRepository = new DepartmentRepository(context);
            _context = context;
            
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository;
    //public AppDbContext Context => _context;

        public int Complete()
        {
         return   _context.SaveChanges();
        }
    }
}
