using Lab_10lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class CustomComparer : IComparer<Goods>
    {
        public int Compare(Goods? product1, Goods? product2)
        {
            if (product1 == null || product2 == null)
                return 0;

            if (product1.Price > product2.Price)
            {
                return 1;
            }
            else if (product1.Price < product2.Price)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
