using ParkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi.Repository.IRepositories
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();

        ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);

        Trail GetTrail(int trailId);

        bool TrailExists(int trailId);

        bool TrailExists(string trailName);

        bool CreateTrail(Trail trail);

        bool UpdateTrail(Trail trail);

        bool DeleteTrail(Trail trail);

        bool DeleteTrail(int trailId);

        bool Save();
    }
}
