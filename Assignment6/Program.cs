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
        public delegate void A(IDictionary<int, int> d, int count);

        public static void Add(IDictionary<int, int> d, int count)
        {
            Random random = new Random(10);

            var watch = Stopwatch.StartNew();


            for (int j = 0; j < count; j++)
            {
                d.Add(random.Next(), j);
            }

            watch.Stop();
            var elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(d.GetType().Name +"(" + count +") = " + elapsedTime);
        }

        public static void Remove(IDictionary<int, int> d, int count)
        {
            Random random = new Random(10);

            var watch = Stopwatch.StartNew();


            for (int j = 0; j < count; j++)
            {
                d.Remove(random.Next());
            }

            watch.Stop();
            var elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(d.GetType().Name + "(" + count + ") = " + elapsedTime);
        }

        public static void ContainsKey(IDictionary<int, int> d, int count)
        {
            Random random = new Random(10);

            var watch = Stopwatch.StartNew();


            for (int j = 0; j < count; j++)
            {
                d.ContainsKey(random.Next());
            }

            watch.Stop();
            var elapsedTime = watch.ElapsedTicks * (1000000.0 / Stopwatch.Frequency);
            Console.WriteLine(d.GetType().Name + "(" + count + ") = " + elapsedTime);
        }

        static void Main(string[] args)
        {
            A add = new A(Add);
            A remove = new A(Remove);
            A containsKey = new A(ContainsKey);

            while (true)
            {
                Console.WriteLine("Add");

                add(new RedBlackTree<int, int>(), 320);
                add((new AVLTree<int, int>()), 320);
                add(new Dictionary<int, int>(), 320);
                Console.WriteLine();
                add(new RedBlackTree<int, int>(), 640);
                add((new AVLTree<int, int>()), 640);
                add(new Dictionary<int, int>(), 640);
                Console.WriteLine();

                add(new RedBlackTree<int, int>(), 1280);
                add((new AVLTree<int, int>()), 1280);
                add(new Dictionary<int, int>(), 1280);
                Console.WriteLine();

                Console.WriteLine("Contains Key");

                containsKey(new RedBlackTree<int, int>(), 320);
                containsKey((new AVLTree<int, int>()), 320);
                containsKey(new Dictionary<int, int>(), 320);
                Console.WriteLine();

                containsKey(new RedBlackTree<int, int>(), 640);
                containsKey((new AVLTree<int, int>()), 640);
                containsKey(new Dictionary<int, int>(), 640);
                Console.WriteLine();

                containsKey(new RedBlackTree<int, int>(), 1280);
                containsKey((new AVLTree<int, int>()), 1280);
                containsKey(new Dictionary<int, int>(), 1280);
                Console.WriteLine();

                Console.WriteLine("Remove");

                remove(new RedBlackTree<int, int>(), 320);
                remove((new AVLTree<int, int>()), 320);
                remove(new Dictionary<int, int>(), 320);
                Console.WriteLine();

                remove(new RedBlackTree<int, int>(), 640);
                remove((new AVLTree<int, int>()), 640);
                remove(new Dictionary<int, int>(), 640);
                Console.WriteLine();

                remove(new RedBlackTree<int, int>(), 1280);
                remove((new AVLTree<int, int>()), 1280);
                remove(new Dictionary<int, int>(), 1280);
                Console.WriteLine();

                Console.ReadLine();
            }
        }
    }
}
