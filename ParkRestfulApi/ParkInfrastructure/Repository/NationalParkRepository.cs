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

        //public bool CreateNationalPark(NationalPark nationalPark)
        //{
        //    _applicationDbContext.NationalParks.Add(nationalPark);

        //    return Save();
        //}

        //public bool DeleteNationalPark(NationalPark nationalPark)
        //{
        //    _applicationDbContext.NationalParks.Remove(nationalPark);

        //    return Save();
        //}

        //public bool DeleteNationalPark(int nationalParkId)
        //{
        //    NationalPark nationalPark = _applicationDbContext.NationalParks.FirstOrDefault(np => np.Id == nationalParkId);

        //    _applicationDbContext.NationalParks.Remove(nationalPark);

        //    return Save();
        //}

        //public NationalPark GetNationalPark(int nationalParkId)
        //{
        //    return _applicationDbContext.NationalParks.FirstOrDefault(np => np.Id == nationalParkId);
        //}

        //public ICollection<NationalPark> GetNationalParks()
        //{
        //    return _applicationDbContext.NationalParks.OrderBy(np => np.Name).ToList();
        //}

        //public bool NationalParkExists(int nationalParkId)
        //{
        //    return _entities.Any(np => np.Id == nationalParkId);
        //}

        public bool ExistsNationalParkByName(string nationalParkName)
        {
            return _entities.Any(np => np.Name.Trim().ToLower() == nationalParkName.Trim().ToLower());
        }

        //public bool Save()
        //{
        //    return _applicationDbContext.SaveChanges() >= 0 ? true : false;
        //}

        //public bool UpdateNationalPark(NationalPark nationalPark)
        //{
        //    _applicationDbContext.NationalParks.Update(nationalPark);
        //    return Save();
        //}
    }
}
