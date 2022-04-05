using AIS.Commands.API;
using AIS.Model.Models;
using Hero.Core.Interfaces;
using Hero.IoC;
using Microsoft.AspNetCore.Mvc;
using Ride.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TenderController
    {
        private readonly IDisposableIoC life;
        private readonly IMappingObject map;

        public TenderController(IDisposableIoC life)
        {
            this.life = life;
            map = life.GetInstance<IMappingObject>();
        }

        [HttpPost]
        public async Task<IActionResult> PostTender(Tender model, CancellationToken cancellation)
        {
            var invoker = life.GetInstance<ICommandInvoker<TenderCommandCUD, bool>>();
            using (var cmd = map.Get<TenderCommandCUD>(model, (src, dest) => dest.CommandProcessor = Commands.CommandProcessor.Add))
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTender(Tender model, CancellationToken cancellation)
        {
            var invoker = life.GetInstance<ICommandInvoker<TenderCommandCUD, bool>>();
            using (var cmd = map.Get<TenderCommandCUD>(model, (src, dest) => dest.CommandProcessor = Commands.CommandProcessor.Add))
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTender(string id, CancellationToken cancellation)
        {
            var invoker = life.GetInstance<ICommandInvoker<TenderCommandCUD, bool>>();
            using (var cmd = new TenderCommandCUD { CommandProcessor = Commands.CommandProcessor.Delete, ID = id })
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTender(string id, CancellationToken cancellation)
        {
            var invoker = life.GetInstance<ICommandInvoker<TenderCommandRA, bool>>();
            using (var cmd = new TenderCommandRA { CommandProcessor = Commands.CommandProcessor.GetOne, ID = id })
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            var invoker = life.GetInstance<ICommandInvoker<TenderCommandRA, IEnumerable<Tender>>>();
            using (var cmd = new TenderCommandRA())
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }
    }
}