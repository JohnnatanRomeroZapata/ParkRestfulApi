using ParkCore.Interfaces.IRepositories;
using ParkInfrastructure.Data;

namespace ParkInfrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailRepository _trailRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public INationalParkRepository NationalParkRepository => _nationalParkRepository ?? new NationalParkRepository(_applicationDbContext);

        public ITrailRepository TrailRepository => _trailRepository ?? new TrailRepository(_applicationDbContext);

        public void Dispose()
        {
            if (_applicationDbContext != null)
            {
                _applicationDbContext.Dispose();
            }
        }
    }
}
