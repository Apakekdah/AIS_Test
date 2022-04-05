using AIS.Commands.API;
using AIS.Model.Models;
using Hero.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.ApiControllers
{
    public partial class TenderController
    {
        [HttpPost("Tender")]
        public async Task<IActionResult> PostTender(Tender model, CancellationToken cancellation)
        {
            var invoker = Life.GetInstance<ICommandInvoker<TenderCommandCUD, bool>>();
            using (var cmd = Map.Get<TenderCommandCUD>(model, (src, dest) => dest.CommandProcessor = Commands.CommandProcessor.Add))
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpPut("Tender")]
        public async Task<IActionResult> UpdateTender(Tender model, CancellationToken cancellation)
        {
            var invoker = Life.GetInstance<ICommandInvoker<TenderCommandCUD, bool>>();
            using (var cmd = Map.Get<TenderCommandCUD>(model, (src, dest) => dest.CommandProcessor = Commands.CommandProcessor.Add))
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpDelete("Tender")]
        public async Task<IActionResult> DeleteTender(string id, CancellationToken cancellation)
        {
            var invoker = Life.GetInstance<ICommandInvoker<TenderCommandCUD, bool>>();
            using (var cmd = new TenderCommandCUD { CommandProcessor = Commands.CommandProcessor.Delete, ID = id })
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpGet("Tender/{id}")]
        public async Task<IActionResult> GetTender(string id, CancellationToken cancellation)
        {
            var invoker = Life.GetInstance<ICommandInvoker<TenderCommandRA, bool>>();
            using (var cmd = new TenderCommandRA { CommandProcessor = Commands.CommandProcessor.GetOne, ID = id })
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }

        [HttpGet("Tender")]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            var invoker = Life.GetInstance<ICommandInvoker<TenderCommandRA, IEnumerable<Tender>>>();
            using (var cmd = new TenderCommandRA())
            {
                return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
            }
        }
    }
}