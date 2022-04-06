using System;

namespace AIS.Model.Models
{
    public class Tender
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Details { get; set; }
        public string CreatorID { get; set; }
    }
}