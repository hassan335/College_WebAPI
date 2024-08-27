using CollegeApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Repository
{
    public class StudentRepository : CollegeRepository<Student> , IStudentRepository
    {

        private readonly CollegeDbContext _db;



        public StudentRepository(CollegeDbContext dbContext):base (dbContext)
        {
            _db = dbContext;
        }

        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            throw new NotImplementedException();
        }

       
    }
}
