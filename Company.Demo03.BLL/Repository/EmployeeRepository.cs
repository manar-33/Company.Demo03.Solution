using Company.Demo03.BLL.Interfaces;
using Company.Demo03.DAL.Data.Contexts;
using Company.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo03.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        // private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            
        }
   

    }
}
