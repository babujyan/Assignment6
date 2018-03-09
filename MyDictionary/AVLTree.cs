using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary
{
    class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private int index;

        private int size;

        private Node<TValue>[] array;

        private Node<TValue> this[int index]
        {
            get
            {
                return this.array[index];
            }
            set
            {
                this.array[index].Key = index;
            }
        }

        public AVLTree(int size = 2)
        {
            this.size = size;
            this.array = new Node<TValue>[this.size];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(int key, TValue value)
        {
            Node<TValue> node = new Node<TValue>(value);

            if (this.index == this.array.Length)
            {
                this.Grow();
            }

            this.array[this.index++] = node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        //private Node<TValue> Search(Node<TValue> node)
        //{
        //    int index = 0;

        //    while (array[index] != null)
        //    {
        //        if (node > array[index])
        //        {
        //            index = array[index].RightChild.Key;
        //        }
        //        else if (node < array[index])
        //        {
        //            index = array[index].LeftChild.Key;
        //        }
        //        else
        //        {
        //            return array[index];
        //        }
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public void Clear()
        //{
        //    this.size = 2;
        //    this.array = new Node<TValue>[this.size];
        //    this.index = 0;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="node"></param>
        ///// <returns></returns>
        //public bool Contains(TValue node)
        //{
        //    int index = 0;

        //    while (array[index] != null)
        //    {
        //        //if (node > array[index].Data)
        //        //{
        //        //    index = array[index].RightChild.Key;
        //        //}
        //        //else if (node < array[index])
        //        //{
        //        //    index = array[index].LeftChild.Key;
        //        //}
        //        //else
        //        //{
        //        //    return true;
        //        //}
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public bool ContainsKey(int key)
        //{
        //    if (key < this.index)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        /// <summary>
        /// 
        /// </summary>
        private void Grow()
        {
            this.array = new Node<TValue>[size << 1];
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="TKey"></typeparam>
        public class Node<TKey>
        {
            public int Key { get; set; }

            private int height;
            public int Height
            {
                get
                {
                    return this.height;
                }

                set
                {
                    this.height = value;
                }
            }

            public readonly TKey Data;

            private Node<TKey> leftChild;
            public Node<TKey> LeftChild
            {
                get
                {
                    return this.leftChild;
                }

                set
                {
                    this.leftChild = value;
                }
            }


            private Node<TKey> rightChild;
            public Node<TKey> RightChild
            {
                get
                {
                    return this.rightChild;
                }

                set
                {
                    this.rightChild = value;
                }
            }


            public Node(TKey data)
            {
                this.Data = data;
                this.height = 0;
                this.rightChild = null;
                this.leftChild = null;
            }

            


        }
    }
}
