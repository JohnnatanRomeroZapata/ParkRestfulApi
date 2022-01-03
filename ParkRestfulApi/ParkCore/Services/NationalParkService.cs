using ParkCore.Interfaces.IRepositories;
using ParkCore.Interfaces.IServices;
using ParkCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkCore.Services
{
    public class NationalParkService : INationalParkService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationalParkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _unitOfWork.NationalParkRepository.GetAll();
        }

        public async Task<NationalPark> GetNationalPark(int nationalParkId)
        {
            return await _unitOfWork.NationalParkRepository.GetById(nationalParkId);
        }

        public async Task<bool> CreateNationalPark(NationalPark nationalPark)
        {
            await _unitOfWork.NationalParkRepository.Create(nationalPark);

            return await Save();
        }

        public async Task<bool> UpdateNationalPark(NationalPark nationalPark)
        {
            _unitOfWork.NationalParkRepository.Update(nationalPark);

            return await Save();
        }

        public async Task<bool> RemoveNationalPark(NationalPark nationalPark)
        {
            await _unitOfWork.NationalParkRepository.Remove(nationalPark);

            return await Save();
        }

        public async Task<bool> ExistsNationalParkById(int nationalParkId)
        {
            return await _unitOfWork.NationalParkRepository.ExistsById(nationalParkId);
        }

        public bool ExistsNationalParkByName(string nationalParkName)
        {
            return _unitOfWork.NationalParkRepository.ExistsNationalParkByName(nationalParkName);
        }

        public async Task<bool> Save()
        {
            return await _unitOfWork.NationalParkRepository.Save();
        }
    }
}
