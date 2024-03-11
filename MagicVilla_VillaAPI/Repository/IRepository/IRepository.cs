using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Get All Generics
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null);
        //Get Single Generics
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracker = true, string? includeProperties = null);
        //Create Generics
        Task CreateAsync(T entity);
        //Remove Generics
        Task RemoveAsync(T entity);
        //Save Changes in DB
        Task SaveAsync();

    }
}
