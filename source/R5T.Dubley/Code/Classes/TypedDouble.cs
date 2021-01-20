using System;


namespace R5T.Dubley
{
    /// <summary>
    /// Allow descendant classes to wrap a double with a specific type.
    /// This is helpful in creating strongly-typed doubles for doubly-typed data. Examples: Longitude and Latitude.
    /// Value is publically read-only (protected writable).
    /// </summary>
    public abstract class TypedDouble : IEquatable<TypedDouble>, IComparable<TypedDouble>
    {
        public const TypedDouble Empty = null;


        #region Static

        public static bool IsEmpty(TypedDouble typedDouble)
        {
            var isEmpty = Object.ReferenceEquals(typedDouble, TypedDouble.Empty);
            return isEmpty;
        }

        // No implicit converstion to value type to avoid accidental client-side execution in EF Core.

        public static bool operator ==(TypedDouble x, TypedDouble y)
        {
            var result = true;

            if (x is object)
            {
                result = x.Equals(y);
            }
            else
            {
                result = y is null;
            }

            return result;
        }

        public static bool operator !=(TypedDouble x, TypedDouble y)
        {
            var output = !(x == y);
            return output;
        }

        #endregion


        public double Value { get; protected set; }


        protected TypedDouble()
        {
        }

        public TypedDouble(double value)
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            var objAsTypedDouble = obj as TypedDouble;

            var isEqual = this.Equals_Internal(objAsTypedDouble);
            return isEqual;
        }

        protected virtual bool Equals_Internal(TypedDouble other)
        {
            var isEqual = this.Value.Equals(other.Value);
            return isEqual;
        }

        public override int GetHashCode()
        {
            var hashCode = this.Value.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var representation = this.Value.ToString();
            return representation;
        }

        public bool Equals(TypedDouble other)
        {
            if (other == null || !other.GetType().Equals(this.GetType()))
            {
                return false;
            }

            var isEqual = this.Equals_Internal(other);
            return isEqual;
        }

        public int CompareTo(TypedDouble other)
        {
            var output = this.Value.CompareTo(other.Value);
            return output;
        }
    }
}
