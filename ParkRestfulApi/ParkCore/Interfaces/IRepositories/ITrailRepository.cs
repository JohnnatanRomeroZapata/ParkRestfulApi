using ParkCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkCore.Interfaces.IRepositories
{
    public interface ITrailRepository : IBaseRepository<Trail>
    {
        ICollection<Trail> GetTrails();

        Task<Trail> GetTrail(int trailId);

        ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);

        bool ExistsTrailByName(string trailName);

        
    }
}
