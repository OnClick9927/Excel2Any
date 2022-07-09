using System;

namespace Excel2Other.Core.__Interface
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class EntityCodeAttribute : System.Attribute
    {
        public readonly int code;

        public EntityCodeAttribute(int code)
        {
            this.code = code;
        }
    }
}
