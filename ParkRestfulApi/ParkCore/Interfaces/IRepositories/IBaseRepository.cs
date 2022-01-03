using ParkCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkCore.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        ICollection<T> GetAll();

        Task<T> GetById(int entityId);

        Task Create(T entity);

        void Update(T entity);

        Task Remove(T entity);

        Task<bool> ExistsById(int entityId);

        Task<bool> Save();
    }
}
