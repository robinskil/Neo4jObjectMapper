using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Neo4j.ObjectMapper.Tests.Models
{
    public class House : IEquatable<House>
    {
        public string Address { get; set; }
        public int Age { get; set; }

        public bool Equals([AllowNull] House other)
        {
            if (other == null)
            {
                return false;
            }
            return Age == other.Age && Address == other.Address;
        }
    }
}
