using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb
{
    public static class StaticDetails
    {
        public static string ParkAPIBaseUrl = "https://localhost:44368/";
        public static string NationalParkAPIPath = ParkAPIBaseUrl + "api/v1/nationalparks/";
        public static string TrailAPIPath = ParkAPIBaseUrl + "api/v1/trails/";
    }
}
