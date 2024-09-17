using AutoMapper;
using MVC_Project.DAL.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
