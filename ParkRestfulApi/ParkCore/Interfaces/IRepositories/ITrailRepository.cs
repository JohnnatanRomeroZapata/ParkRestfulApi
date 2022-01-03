using ParkCore.Models;

namespace ParkCore.Interfaces.IRepositories
{
    public interface ITrailRepository : IBaseRepository<Trail>
    {
        bool ExistsTrailByName(string trailName);
    }
}
