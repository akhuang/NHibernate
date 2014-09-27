using System;

namespace Eg.AttributeValidation
{
    public abstract class Entity
    {
        
        public virtual Guid Id { get; protected set;}

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public virtual bool Equals(Entity other)
        {
            return other != null && other.Id == Id;
        }

    }
}
