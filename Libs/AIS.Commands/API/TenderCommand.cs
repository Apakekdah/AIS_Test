using AIS.Data.Entity;

namespace AIS.Commands.API
{
    public class TenderCommandCUD : BaseCommand
    {
        public Tender Tender { get; set; }

        public string ID { get; set; }
    }

    public class TenderCommandRA : BaseCommand
    {
        public string ID { get; set; }
    }
}