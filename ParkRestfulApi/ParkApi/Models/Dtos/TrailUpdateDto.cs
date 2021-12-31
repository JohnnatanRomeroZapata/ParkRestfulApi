using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkApi.Models.Trail;

namespace ParkApi.Models.Dtos
{
    public class TrailUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        public int NationalParkId { get; set; }

    }
}
