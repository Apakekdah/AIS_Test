using AIS.Data.Entity;

namespace AIS.Commands.API
{
    public class TenderCommandCUD : BaseCommand
    {
        public CommandProcessor CommandProcessor { get; set; }

        public Tender Tender { get; set; }

        public string ID { get; set; }
    }

    public class TenderCommandRA : BaseCommand
    {
        public CommandProcessor CommandProcessor { get; set; }

        public string ID { get; set; }
    }
}