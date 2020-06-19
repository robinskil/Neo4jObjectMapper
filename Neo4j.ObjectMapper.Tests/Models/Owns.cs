using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Neo4j.ObjectMapper.Tests.Models
{
    public class Owns : IEquatable<Owns>
    {
        public DateTime OwnedFrom { get; set; }
        public DateTime OwnedTill { get; set; }
        public House House { get; set; }

        public bool Equals([AllowNull] Owns other)
        {
            if (other != null)
            {
                return other.House.Equals(House) && other.OwnedFrom == OwnedFrom && other.OwnedTill == other.OwnedTill;
            }
            return false;
        }
    }
}
