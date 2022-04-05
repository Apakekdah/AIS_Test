using Ride.Attributes.DB;

namespace AIS.Data.Entity
{
    [TableName("td_User")]
    public class User
    {
        [FieldKey]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}