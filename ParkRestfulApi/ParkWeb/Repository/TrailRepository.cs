using ParkCore.Models;
using ParkWeb.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkWeb.Repository
{
    public class TrailRepository : BaseRepository<Trail>, ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TrailRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
