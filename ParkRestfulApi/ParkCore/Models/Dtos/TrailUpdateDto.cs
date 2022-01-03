using static ParkCore.Models.Trail;

namespace ParkCore.Models.Dtos
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
