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
    public class DepartmentRepository : IDepartmentRepository
    {
        //Open Connection to DataBase
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) //Ask CLR to create Object from AppDbContext
        {
            _context = context;
        }
        public IEnumerable<Department> GetAll()
        {
          return  _context.Departments.ToList();
        }

        public Department Get(int id)
        {
            //return _context.Departments.FirstOrDefault(D=>D.Id==id);
            return _context.Departments.Find(id);
        }

        public int Add(Department entity)
        {
           _context.Departments.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }

     

      
    }
}
