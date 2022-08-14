using System;

namespace Excel2Any
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
