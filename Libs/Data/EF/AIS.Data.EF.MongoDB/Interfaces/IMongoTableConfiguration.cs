﻿namespace AIS.Data.EF.MongoDB.Interfaces
{
    interface IMongoTableConfigurable<T>
        where T : class, new()
    {
        void Apply();
    }
}