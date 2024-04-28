using CollegeApp.Data;

namespace CollegeApp.Repository
{
    public interface IStudentRepository
    {

        Task<List<Student>> GetAllStudentsAsync();

        Task< Student> GetStudentByIdAsync(int id,bool TrackingFlag=false);


       Task < Student> GetStudentByNameAsync(string name);



      Task < bool> DeleteStudentByIdAsync(int Id);


       Task<int> SaveStudent(Student st);


       Task <int>UpdateStudentAsync(Student st);


        //void UpdateStudentPatch(Student st);






    }
}
