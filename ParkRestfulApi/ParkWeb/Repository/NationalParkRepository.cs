using ParkCore.Models.Dtos;
using ParkWeb.Repository.Interfaces;
using System.Net.Http;

namespace ParkWeb.Repository
{
    public class NationalParkRepository : BaseRepository<NationalParkDto>, INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NationalParkRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
