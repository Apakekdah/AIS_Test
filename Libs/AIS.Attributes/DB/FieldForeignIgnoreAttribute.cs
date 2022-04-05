using System;

namespace AIS.Attributes.DB
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FieldForeignIgnoreAttribute : Attribute
    {
    }
}