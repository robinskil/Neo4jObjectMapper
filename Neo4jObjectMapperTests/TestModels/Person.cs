using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace NeoObjectMapperTests
{
    //Guid is not supported
    public class Person : IEquatable<Person>
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<Owns> Owns { get; set; }

        public bool Equals([AllowNull] Person other)
        {
            if(other != null)
            {
                if(!(Id == other.Id && Age == other.Age && Name == other.Name && Salary == other.Salary && DateOfBirth == other.DateOfBirth))
                {
                    return false;
                }
                if(!((other.Owns == null && Owns == null) || (other.Owns != null && Owns != null)))
                {
                    return false;
                }
                if(other.Owns.Count != Owns.Count)
                {
                    return false;
                }
                foreach (var own in Owns)
                {
                    if(!other.Owns.Any(oo => own.Equals(oo)))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
