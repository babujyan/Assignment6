using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    class Program
    {

        public delegate void a();

        static void b()
        {

        }

        static void Main(string[] args)
        {
            a d;
            d = b;
            

            Dictionary<a ,a> v = new Dictionary<a, a>();
            v.Add(d, d);
        }
    }
}
