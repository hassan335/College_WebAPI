using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;

namespace CollegeApp.AutoMapperConfig
{
    public class AutoMapperConfig :Profile
    {



        public AutoMapperConfig()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }



      







    }
}
