using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkWeb.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string url);

        Task<T> GetAsync(string url, int? id);

        Task<bool> CreateAsync(string url, T objToCreate);

        Task<bool> UpdateAsync(string url, int id, T objToUpdate);

        Task<bool> DeleteAsync(string url, int id);
    }
}
