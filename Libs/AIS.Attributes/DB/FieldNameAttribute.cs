using System;

namespace AIS.Attributes.DB
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(string name = null)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public int Order { get; set; } = -1;
    }
}
