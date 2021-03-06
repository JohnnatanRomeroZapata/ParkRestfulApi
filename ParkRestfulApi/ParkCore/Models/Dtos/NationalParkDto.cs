using System;
using System.ComponentModel.DataAnnotations;

namespace ParkCore.Models.Dtos
{
    public class NationalParkDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }

        public DateTime EstablishedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }
    }
}
