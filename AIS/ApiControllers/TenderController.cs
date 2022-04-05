using Hero.IoC;
using Microsoft.AspNetCore.Mvc;

namespace AIS.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController
    {
        public TenderController(IDisposableIoC life)
        {
            Life = life;
        }

        protected IDisposableIoC Life { get; private set; }
    }
}