using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;

namespace CollegeApp.AutoMapperConfig
{
    public class AutoMapperConfig :Profile
    {



        public AutoMapperConfig()
        {
            //CreateMap<Student, StudentDTO>().ReverseMap().ForMember(x => x.Name, y => y.MapFrom(z => z.StudentName));// StudentDTO to Student

            //CreateMap<Student, StudentDTO>().ForMember(x => x.StudentName, y => y.MapFrom(z => z.Name)).ReverseMap().ForMember(x => x.Email, x => x.Ignore());// Student to StudentDTO for student name only



            CreateMap<Student, StudentDTO>().ForMember(x => x.Address, y => y.MapFrom(z => String.IsNullOrEmpty(z.Address) ? "No Address found in records" : z.Address)).ReverseMap(); //Student to StudentDTO








        }











    }
}
