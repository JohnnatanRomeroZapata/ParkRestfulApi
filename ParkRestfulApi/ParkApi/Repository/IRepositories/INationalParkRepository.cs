using ParkApi.Models;
using System.Collections.Generic;

namespace ParkApi.Repository.IRepositories
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();

        NationalPark GetNationalPark(int nationalParkId);

        bool NationalParkExists(int nationalParkId);

        bool NationalParkExists(string nationalParkName);

        bool CreateNationalPark(NationalPark nationalPark);

        bool UpdateNationalPark(NationalPark nationalPark);

        bool DeleteNationalPark(NationalPark nationalPark);

        bool DeleteNationalPark(int nationalParkId);

        bool Save();
    }
}
