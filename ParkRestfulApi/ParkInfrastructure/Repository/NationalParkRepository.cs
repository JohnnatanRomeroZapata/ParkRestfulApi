using ParkCore.Interfaces.IRepositories;
using ParkCore.Models;
using ParkInfrastructure.Data;
using System.Linq;

namespace ParkInfrastructure.Repository
{
    public class NationalParkRepository : BaseRepository<NationalPark>, INationalParkRepository
    {
        public NationalParkRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        //public ICollection<NationalPark> GetNationalParks()
        //{
        //    return _applicationDbContext.NationalParks.OrderBy(np => np.Name).ToList();
        //}

        public bool ExistsNationalParkByName(string nationalParkName)
        {
            return _entities.Any(np => np.Name.Trim().ToLower() == nationalParkName.Trim().ToLower());
        }
    }
}
