using System;
using System.Collections.Generic;

namespace SystemDataModel
{
    public class IdDto : IEquatable<IdDto>
    {
        public int? Id { get; }

        public IdDto() { }
        internal IdDto(int id) => Id = id;

        public override bool Equals(object obj)
        {
            return Equals(obj as IdDto);
        }

        public bool Equals(IdDto other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(IdDto left, IdDto right)
        {
            return EqualityComparer<IdDto>.Default.Equals(left, right);
        }

        public static bool operator !=(IdDto left, IdDto right)
        {
            return !(left == right);
        }
    }
}
