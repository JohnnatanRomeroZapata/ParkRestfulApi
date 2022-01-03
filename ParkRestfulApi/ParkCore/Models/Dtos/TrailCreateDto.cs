using System.ComponentModel.DataAnnotations;
using static ParkCore.Models.Trail;

namespace ParkCore.Models.Dtos
{
    public class TrailCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        public int NationalParkId { get; set; }
    }
}
