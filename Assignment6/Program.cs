using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDictionary;

namespace Assignment6
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            double elapsedTime;
            Random random = new Random(10);
            RedBlackTree<int, int>[] a = new RedBlackTree<int, int>[1000];
            var watch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
            {
                random = new Random(10);
                a[i] = new RedBlackTree<int, int>();
                watch = Stopwatch.StartNew();

                
                for (int j = 0; j < i; j++)
                {
                    a[i].Add(random.Next(), random.Next());
                }
                watch.Stop();
                elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
                Console.WriteLine(elapsedTime);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);


            //RedBlackTree<int, int> a = new RedBlackTree<int, int>();
            //watch.Stop();
            //elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            //Console.WriteLine("Creation time RB tree = "+ elapsedTime);




            //Random random = new Random(1);

            //watch = Stopwatch.StartNew();

            //for (int i = 0; i<10000;i++)
            //{   
            //    a.Add(random.Next(), random.Next());
            //}
            //watch.Stop();
            //elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            //Console.WriteLine("Adding 10000 elemts into RB tree. time = "+elapsedTime);

            //watch = Stopwatch.StartNew();

            var b = new Dictionary<int, int>();
            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine("creation of dicinary. time = " + elapsedTime);
            
            watch = Stopwatch.StartNew();

            random = new Random(1);
            for (int i = 0; i < 10000; i++)
            {
                b.Add(random.Next(), random.Next());
            }
            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            Console.WriteLine("Adding 10000 elemts into dicinar. time = " + elapsedTime);

            //random = new Random(1);
            //var r = random.Next();
            //for(int i= 0; i < 10000;i++ )
            //{
            //    r = random.Next();
            //    watch = Stopwatch.StartNew();
            //    a.Remove(r);
            //    watch.Stop();
            //    elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            //    Console.WriteLine("remove a random elemt from rb tree. time = " + elapsedTime);

            //    watch = Stopwatch.StartNew();
            //    b.Remove(r);
            //    watch.Stop();
            //    elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);

            //    Console.WriteLine("removel from dicinary . time = " + elapsedTime);

            //    watch = Stopwatch.StartNew();

            //    watch.Stop();
            //    elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);



            //}

            Dictionary<int, string> f = new Dictionary<int, string>();
            KeyValuePair<int, string> keyValuePair = new KeyValuePair<int, string>(4, "jsnd0");
            
        }

        private static RedBlackTree<int, int> RedBlackTree()
        {
            throw new NotImplementedException();
        }
    }
}
