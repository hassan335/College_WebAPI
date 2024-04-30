using CollegeApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollegeApp.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDbContext _db;

        private DbSet<T> _dbset;
        public CollegeRepository(CollegeDbContext dbContext)
        {
                this._db = dbContext;   
                 _dbset = dbContext.Set<T>();   

        }


        public async Task<T> SaveStudent(T dbrecord)
        {
            await _dbset.AddAsync(dbrecord);
            await _db.SaveChangesAsync();

            return dbrecord;


        }

        public async Task<bool> DeleteTByIdAsync(T dbrecord)
        {

            try
            {
                _dbset.Remove(dbrecord);
                

                _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

                return false;
            }


        }

        public async Task<List<T>> GetAllTAsync()
        {
            return await _dbset.ToListAsync();
        }


        public async Task<T> GetTByIdAsync(Expression<Func<T,bool>> filter, bool TrackinFlag = false)
        {
            if (TrackinFlag)

                return await _dbset.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            else
                return await _dbset.Where(filter).FirstOrDefaultAsync();




        }
        public async Task<T> GetTByNameAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbset.Where(filter).FirstOrDefaultAsync();
        }


        public async Task<T> UpdateTAsync(T dbrecord)
        {
            _db.Update(dbrecord);
            await _db.SaveChangesAsync();

            return dbrecord;

        }








    }

}
