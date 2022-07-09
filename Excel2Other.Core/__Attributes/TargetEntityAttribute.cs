using System;

namespace Excel2Other.Core.__Interface
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class EntityAttribute : System.Attribute
    {
        public readonly Type entityType;

        public EntityAttribute(Type entityType)
        {
            this.entityType = entityType;
        }
    }
}
