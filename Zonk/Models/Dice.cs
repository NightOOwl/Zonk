using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zonk.Models
{
    public class Dice: IComparable<Dice>, IEquatable<Dice>
    {       
        public int Value { get; private set;}
        public void Throw()
        {
            var rnd = new Random();
            Value = rnd.Next(1,7);
        }
        public override string ToString()
        {
            return Value.ToString() ?? "Кость не бросали";
        }

        public int CompareTo(Dice? other)
        {
            return Value.CompareTo(other?.Value);
        }

        public bool Equals(Dice? other)
        {
            return Value == other?.Value;
        }
    }
}
