using System;

namespace ParkApi.Models.Dtos
{
    public class NationalParkDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public DateTime EstablishedOn { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
