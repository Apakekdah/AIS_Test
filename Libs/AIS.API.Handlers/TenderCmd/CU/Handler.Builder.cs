using AIS.Commands.API;
using Ride.Handlers.Handlers;
using System;

namespace AIS.API.Handlers.TenderCmd.CU
{
    public partial class Handler
    {
        public const string DEFAULT_NAME = "TenderCreateUpdateDelete";

        public static Builder CreateBuilder()
        {
            return new Builder(DEFAULT_NAME, c => new Handler(c));
        }

        public class Builder : BuilderBase<Builder>
        {
            private readonly string name;
            private readonly Func<Config, Handler> factory;

            public Builder(string name, Func<Config, Handler> factory)
            {
                this.name = name;
                this.factory = factory;
            }

            protected override string DefaultName => name;

            public override CommandHandlerBase<TenderCommandCU, bool> Build()
            {
                return factory(GetConfig());
            }
        }
    }
}