using System;

namespace Excel2Other
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
