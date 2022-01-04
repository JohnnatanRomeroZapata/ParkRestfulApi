using Microsoft.EntityFrameworkCore;
using ParkCore.Interfaces.IRepositories;
using ParkCore.Models;
using ParkInfrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkInfrastructure.Repository
{
    public class TrailRepository : BaseRepository<Trail>, ITrailRepository
    {
        public TrailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
        
        public ICollection<Trail> GetTrails()
        {
            return _entities.Include(t => t.NationalPark).OrderBy(trail => trail.Name).ToList();
        }

        public async Task<Trail> GetTrail(int trailId)
        {
            return await _entities.Include(trail => trail.NationalPark).FirstOrDefaultAsync(trail => trail.Id == trailId);
        }

        public bool ExistsTrailByName(string trailName)
        {
            return _entities.Any(trail => trail.Name.Trim().ToLower() == trailName.Trim().ToLower());
        }

        //public ICollection<Trail> GetTrailsInNationalPark(int trailId)
        //{
        //    return _applicationDbContext.Trails.Include(t => t.NationalPark).Where(t => t.NationalParkId == trailId).ToList();
        //}
    }
}
