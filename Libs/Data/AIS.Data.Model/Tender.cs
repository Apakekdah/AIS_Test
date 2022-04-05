using Ride.Attributes.DB;
using System;

namespace AIS.Data.Entity
{
    [TableName("td_Tender")]
    public class Tender
    {
        [FieldKey]
        public string ID { get; set; }
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Details { get; set; }

        [FieldIndex("idx_tender_cid")]
        public string CreatorID { get; set; }
    }
}