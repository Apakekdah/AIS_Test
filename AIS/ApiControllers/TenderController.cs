using Hero.IoC;
using Microsoft.AspNetCore.Mvc;
using Ride.Interfaces;

namespace AIS.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TenderController
    {
        public TenderController(IDisposableIoC life)
        {
            Life = life;
            Map = life.GetInstance<IMappingObject>();
        }

        protected IDisposableIoC Life { get; private set; }
        protected IMappingObject Map { get; private set; }
    }
}