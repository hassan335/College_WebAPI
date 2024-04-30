using CollegeApp.Data;
using System.Linq.Expressions;

namespace CollegeApp.Repository
{
    public interface ICollegeRepository<T>
    {

        Task<List<T>> GetAllTAsync();

        Task<T> GetTByIdAsync(Expression<Func<T, bool>> filter, bool TrackingFlag = false);


        Task<T> GetTByNameAsync(Expression<Func<T, bool>> filter);



        Task<bool> DeleteTByIdAsync(T dbrecord);


        Task<T> SaveStudent(T dbrecord);


        Task<T> UpdateTAsync(T dbrecord);
    }
}
