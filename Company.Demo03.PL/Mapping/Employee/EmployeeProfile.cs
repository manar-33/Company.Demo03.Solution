using AutoMapper;
using Company.Demo03.DAL.Models;
using Company.Demo03.PL.ViewModels.Employees;



namespace Company.Demo03.PL.Mapping.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap < Company.Demo03.DAL.Models.Employee , EmployeeViewModel > ().ReverseMap();
        }
    }
}
