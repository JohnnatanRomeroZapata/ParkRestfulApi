using ParkCore.Interfaces.IRepositories;
using ParkCore.Models;
using ParkInfrastructure.Data;
using System.Linq;

namespace ParkInfrastructure.Repository
{
    public class TrailRepository : BaseRepository<Trail>, ITrailRepository
    {
        public TrailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        //public TrailRepository(ApplicationDbContext applicationDbContext)
        //{
        //    _applicationDbContext = applicationDbContext;
        //}

        //public bool CreateTrail(Trail trail)
        //{
        //    _applicationDbContext.Trails.Add(trail);

        //    return Save();
        //}

        //public bool DeleteTrail(Trail trail)
        //{
        //    _applicationDbContext.Trails.Remove(trail);

        //    return Save();
        //}

        //public bool DeleteTrail(int trailId)
        //{
        //    Trail trail = _applicationDbContext.Trails.FirstOrDefault(trail => trail.Id == trailId);

        //    _applicationDbContext.Trails.Remove(trail);

        //    return Save();
        //}

        //public Trail GetTrail(int trailId)
        //{
        //    return _applicationDbContext.Trails.Include(t => t.NationalPark).FirstOrDefault(trail => trail.Id == trailId);
        //}

        //public ICollection<Trail> GetTrails()
        //{
        //    return _applicationDbContext.Trails.Include(t => t.NationalPark).OrderBy(trail => trail.Name).ToList();
        //}

        //public bool TrailExists(int trailId)
        //{
        //    return _applicationDbContext.Trails.Any(trail => trail.Id == trailId);
        //}

        public bool ExistsTrailByName(string trailName)
        {
            return _entities.Any(trail => trail.Name.Trim().ToLower() == trailName.Trim().ToLower());
        }

        //public bool Save()
        //{
        //    return _applicationDbContext.SaveChanges() >= 0 ? true : false;
        //}

        //public bool UpdateTrail(Trail trail)
        //{
        //    _applicationDbContext.Trails.Update(trail);
        //    return Save();
        //}

        //public ICollection<Trail> GetTrailsInNationalPark(int trailId)
        //{
        //    return _applicationDbContext.Trails.Include(t => t.NationalPark).Where(t => t.NationalParkId == trailId).ToList();
        //}
    }
}
