using Ride.Attributes.Validator;
using System;
using System.ComponentModel.DataAnnotations;

namespace AIS.Model.Models
{
    public class Tender
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ReferenceNumber { get; set; }
        [Required]
        [DateValidator(-1)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string CreatorID { get; set; }
    }
}