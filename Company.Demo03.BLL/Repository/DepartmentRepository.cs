using Company.Demo03.BLL.Interfaces;
using Company.Demo03.DAL.Data.Contexts;
using Company.Demo03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo03.BLL.Repository
{
    public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository
    {
        //Open Connection to DataBase
       // private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context):base(context) //Ask CLR to create Object from AppDbContext
        {
            
        }
  

     

      
    }
}
