using System.ComponentModel.DataAnnotations;
using static ParkCore.Models.Trail;

namespace ParkCore.Models.Dtos
{
    public class TrailDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        public int NationalParkId { get; set; }

        public NationalParkDto NationalPark { get; set; }
    }
}
