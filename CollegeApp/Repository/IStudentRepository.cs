using CollegeApp.Data;

namespace CollegeApp.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {

        Task <List<Student>> GetStudentsByFeeStatusAsync(int feeStatus);



    }
}
