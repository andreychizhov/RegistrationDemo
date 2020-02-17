namespace PM.Infrastructure
{
    public abstract class Entity<TId> : IEntityWithTypedId<TId>
    {
        public TId Id { get; set; }

        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }
    }

    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }

        bool IsTransient();
    }

    public abstract class ValueEntity : Entity<int>
    {
        protected bool Equals(ValueEntity other)
        {
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return obj is ValueEntity && Equals((ValueEntity)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("Id = {0}", Id);
        }
    }

    public abstract class DictionaryEntity : ValueEntity
    {

    }
}
