using System;

namespace AIS.Attributes.DB
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class FieldIndexAttribute : Attribute
    {
        public FieldIndexAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public bool Ascending { get; set; } = true;
        public bool Unique { get; set; }
    }
}