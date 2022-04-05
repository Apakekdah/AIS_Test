using AIS.Attributes.DB;
using System;

namespace AIS.Data.Model
{
    [TableName("td_FieldData")]
    public class Tender
    {

        [FieldKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Details { get; set; }

        [FieldIndex("idx_tender_crtr")]
        public string CreatorID { get; set; }
    }
}