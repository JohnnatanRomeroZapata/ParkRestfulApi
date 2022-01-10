using ParkCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkCore.Interfaces.IServices
{
    public interface ITrailService
    {
        ICollection<Trail> GetTrails();

        Task<Trail> GetTrail(int entityId);

        ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);

        Task<bool> CreateTrail(Trail entity);

        Task<bool> UpdateTrail(Trail entity);

        Task<bool> RemoveTrail(Trail entity);

        Task<bool> ExistsTrailById(int entityId);

        bool ExistsTrailByName(string entityName);

        Task<bool> Save();
    }
}
