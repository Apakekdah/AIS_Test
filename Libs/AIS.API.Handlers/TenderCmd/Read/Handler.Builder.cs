﻿using AIS.Commands.API;
using Ride.Handlers.Handlers;
using System;
using System.Collections.Generic;

namespace AIS.API.Handlers.TenderCmd.Read
{
    public partial class Handler
    {
        public const string DEFAULT_NAME = "TenderReader";

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

            public override CommandHandlerBase<TenderCommandRA, IEnumerable<Model.Models.Tender>> Build()
            {
                return factory(GetConfig());
            }
        }
    }
}