using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KACCloudContextLibrary.Domain
{
    public abstract class EntityBase : IEquatable<EntityBase>
    {
        public Guid ID { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastEditDate { get; set; }

        public bool Equals(EntityBase? other)
        {
            if (ReferenceEquals(objA: null, other)) return false;
            return ReferenceEquals(this, other) || Equals(ID, other.ID);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(objA: null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EntityBase)obj);
        }

        public override int GetHashCode() => ID.GetHashCode();

        public static bool operator ==(EntityBase? left, EntityBase? right) => Equals(left, right);

        public static bool operator !=(EntityBase? left, EntityBase? right) => !Equals(left, right);

        public void SetCreated() => CreationDate = DateTime.UtcNow;

        public void SetUpdated() => LastEditDate = DateTime.UtcNow;
    }
}
