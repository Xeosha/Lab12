using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    using System;

    public struct CloneableInt : ICloneable, IComparable
    {
        public int value;

        public CloneableInt(int value)
        {
            this.value = value;
        }

        public object Clone()
        {
            return new CloneableInt(value);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) 
                return 1;

            if (obj is CloneableInt cloneableInt)
            {
                return value.CompareTo(cloneableInt.value);
            }

            throw new ArgumentException("Не удалось преобразовать");
        }

        public override string ToString()
        {
            return value.ToString();
        }


        public static explicit operator CloneableInt(int intValue)
        {
            return new CloneableInt(intValue);
        }

        public static implicit operator CloneableInt(long longValue)
        {
            return new CloneableInt((int)longValue);
        }

        public static explicit operator int(CloneableInt cloneableInt)
        {
            return cloneableInt.value;
        }


    }
}
