using Microsoft.EntityFrameworkCore;
using ParkCore.Interfaces.IRepositories;
using ParkCore.Models;
using ParkInfrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkInfrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _entities = applicationDbContext.Set<T>();
        }

        public ICollection<T> GetAll()
        {
            return _entities.ToList();
        }

        public async Task<T> GetById(int entityId)
        {
            return await _entities.FindAsync(entityId);
        }

        public async Task Create(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Remove(T entity)
        {
            T entityToDelete = await GetById(entity.Id);
            _entities.Remove(entityToDelete);

        }

        public async Task<bool> ExistsById(int entityId)
        {
            T entity = await _entities.FindAsync(entityId);

            return entity != null ? true : false;
        }

        public async Task<bool> Save()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
