using CollegeApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Repository
{
    public class StudentRepository : IStudentRepository
    {

        private readonly CollegeDbContext _db;



        public StudentRepository(CollegeDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> DeleteStudentByIdAsync(int Id)
        {

            try
            {
                //Student s = await _db.Students.Where(x => x.Id == Id).FirstOrDefaultAsync();


                Student s =  _db.Students.Where(x => x.Id == Id).FirstOrDefault();


                if (s == null)
                    throw new ArgumentNullException("No Student found with this id" + s.Id);





                _db.Students.Remove(s);
                //await _db.SaveChangesAsync();

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

                return false;
            }

          
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return  await _db.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id,bool TrackinFlag=false)
        {
            if (TrackinFlag)
            
                return  await _db.Students.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

            else
                return await _db.Students.Where(x => x.Id == id).SingleOrDefaultAsync();




        }

        public async Task<Student> GetStudentByNameAsync(string name)
        {
            return await _db.Students.Where(x => x.Name.ToLower() == name.ToLower()).SingleOrDefaultAsync();
        }

        public async Task <int> SaveStudent(Student st)
        {
          await  _db.Students.AddAsync(st);
            await _db.SaveChangesAsync();

            return st.Id;


        }

        public async  Task<int> UpdateStudentAsync(Student st)
        {
                   _db.Update(st);
            await _db.SaveChangesAsync();

                return st.Id;

}

        public void UpdateStudentPatch(Student st)
        {
            throw new NotImplementedException();
        }
    }
}
