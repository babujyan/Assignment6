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
        static void Add< T , TKey, TValue>(T t) where T : IDictionary<TKey,TValue>
        {

        }

        static void Main(string[] args)
        {            
            double elapsedTime;
            Random random = new Random(10);
            AVLTree<int, int>[] a = new AVLTree<int, int>[4];
            RedBlackTree<int, int>[] b = new RedBlackTree<int, int>[4];
            Dictionary<int, int>[] c = new Dictionary<int, int>[4];
            var watch = Stopwatch.StartNew();
            

            // AVL
            random = new Random(10);
            a[0] = new AVLTree<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 320; j++)
            {
                a[0].Add(random.Next(), j);
            }
            
            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);

            //RedBlack
            random = new Random(10);
            b[0] = new RedBlackTree<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 320; j++)
            {
                b[0].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);

            //Hash
            random = new Random(10);
            c[0] = new Dictionary<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 320; j++)
            {
                c[0].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);

            //AVL
            random = new Random(10);
            a[1] = new AVLTree<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 640; j++)
            {
                a[1].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);

            //RedBlack
            random = new Random(10);
            b[1] = new RedBlackTree<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 640; j++)
            {
                b[1].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);


            //Hash
            random = new Random(10);
            c[1] = new Dictionary<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 640; j++)
            {
                c[1].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);


            //AVL
            random = new Random(10);
            a[2] = new AVLTree<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 1280; j++)
            {
                a[2].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);



            //RedBlack
            random = new Random(10);
            b[2] = new RedBlackTree<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 1280; j++)
            {
                b[2].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);


            //Hash
            random = new Random(10);
            c[2] = new Dictionary<int, int>();
            watch = Stopwatch.StartNew();


            for (int j = 0; j < 1280; j++)
            {
                c[2].Add(random.Next(), j);
            }

            watch.Stop();
            elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(elapsedTime);
        }                       
    }
}
