using ParkCore.Interfaces.IRepositories;
using ParkCore.Interfaces.IServices;
using ParkCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkCore.Services
{
    public class TrailService : ITrailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Trail> GetTrails()
        {
            return _unitOfWork.TrailRepository.GetAll();
        }

        public async Task<Trail> GetTrail(int trailId)
        {
            return await _unitOfWork.TrailRepository.GetById(trailId);
        }

        public async Task<bool> CreateTrail(Trail trail)
        {
            await _unitOfWork.TrailRepository.Create(trail);

            return await Save();
        }

        public async Task<bool> UpdateTrail(Trail trail)
        {
            _unitOfWork.TrailRepository.Update(trail);

            return await Save();
        }

        public async Task<bool> RemoveTrail(Trail trail)
        {
            await _unitOfWork.TrailRepository.Remove(trail);

            return await Save();
        }

        public async Task<bool> ExistsTrailById(int trailId)
        {
            return await _unitOfWork.TrailRepository.ExistsById(trailId);
        }

        public bool ExistsTrailByName(string trailName)
        {
            return _unitOfWork.TrailRepository.ExistsTrailByName(trailName);
        }

        public async Task<bool> Save()
        {
            return await _unitOfWork.TrailRepository.Save();
        }
    }
}
