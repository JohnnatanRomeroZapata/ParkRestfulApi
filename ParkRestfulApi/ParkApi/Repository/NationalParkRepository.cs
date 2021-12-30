using ParkApi.Data;
using ParkApi.Models;
using ParkApi.Repository.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace ParkApi.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NationalParkRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _applicationDbContext.NationalParks.Add(nationalPark);

            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _applicationDbContext.NationalParks.Remove(nationalPark);

            return Save();
        }

        public bool DeleteNationalPark(int nationalParkId)
        {
            NationalPark nationalPark = _applicationDbContext.NationalParks.FirstOrDefault(np => np.Id == nationalParkId);

            _applicationDbContext.NationalParks.Remove(nationalPark);

            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _applicationDbContext.NationalParks.FirstOrDefault(np => np.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _applicationDbContext.NationalParks.OrderBy(np => np.Name).ToList();
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _applicationDbContext.NationalParks.Any(np => np.Id == nationalParkId);
        }

        public bool NationalParkExists(string nationalParkName)
        {
            return _applicationDbContext.NationalParks.Any(np => np.Name.Trim().ToLower() == nationalParkName.Trim().ToLower());
        }

        public bool Save()
        {
            return _applicationDbContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _applicationDbContext.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
