using ParkCore.Models;

namespace ParkCore.Interfaces.IRepositories
{
    public interface INationalParkRepository : IBaseRepository<NationalPark>
    {
        bool ExistsNationalParkByName(string nationalParkName);
    }
}
