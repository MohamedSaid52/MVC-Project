using AutoMapper;
using MVC.DAL.Entities;
using MVC.PL.Models;

namespace MVC.PL.Mapping
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
