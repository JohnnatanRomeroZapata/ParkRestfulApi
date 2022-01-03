using ParkCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkCore.Interfaces.IServices
{
    public interface INationalParkService
    {
        ICollection<NationalPark> GetNationalParks();

        Task<NationalPark> GetNationalPark(int entityId);

        Task<bool> CreateNationalPark(NationalPark entity);

        Task<bool> UpdateNationalPark(NationalPark entity);

        Task<bool> RemoveNationalPark(NationalPark entity);

        Task<bool> ExistsNationalParkById(int entityId);

        bool ExistsNationalParkByName(string entityName);

        Task<bool> Save();
    }
}
