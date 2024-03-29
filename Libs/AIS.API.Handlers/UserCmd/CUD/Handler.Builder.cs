﻿using AIS.Commands.API;
using Ride.Handlers.Handlers;
using System;

namespace AIS.API.Handlers.UserCmd.CUD
{
    public partial class Handler
    {
        public const string DEFAULT_NAME = "UserCRUD";

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

            public override CommandHandlerBase<UserCommand, Model.Models.User> Build()
            {
                return factory(GetConfig());
            }
        }
    }
}