using System.ComponentModel.DataAnnotations;

namespace ParkCore.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
