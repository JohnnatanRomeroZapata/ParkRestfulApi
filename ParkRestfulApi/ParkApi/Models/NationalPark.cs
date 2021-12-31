﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ParkApi.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalPark

    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }

        public DateTime EstablishedOn { get; set; }

        public DateTime CreatedOn { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
