using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary
{
    class RedBlackTree<TKey, TValue> where TKey:IComparable<TKey>
    {
        class RedBlackNode
        {
            private readonly TValue nodeValue;
            private readonly TKey nodeKey;


            private RedBlackNode parentNode;
            private RedBlackNode leftNode;
            private RedBlackNode rightNode;

            public TKey NodeKey
            {
                get { return this.nodeKey;}
            }

            public TValue NodeValue
            {
                get { return this.nodeValue;}
            }

            private RedBlackNode ParentNode
            {
                get
                {
                    return this.parentNode;
                }
                set
                {
                    this.parentNode = value;
                }
            }

            private RedBlackNode LeftNode
            {
                get
                {
                    return this.leftNode;
                }
                set
                {
                    this.leftNode = value;
                }
            }
            private RedBlackNode RightNode
            {
                get
                {
                    return this.rightNode;
                }
                set
                {
                    this.rightNode = value; 
                }
            }
                
            public NodeColor Color { get; set; }

            public Direction ParentDirection
            {
               get
               {
                   if (ParentNode == null || NodeKey.CompareTo(ParentNode.NodeKey) > 0)
                   {
                       return Direction.LEFT;
                   }

                   return Direction.RIGHT;
               }
            }

            public bool IsRoot
            {
                get { return (this.ParentNode == null); }
            }

            public bool IsLeaf
            {
                get { return (this.LeftNode == null && this.RightNode == null); }
            }

            public RedBlackNode(TKey key, RedBlackNode rightNode, RedBlackNode leftNode)
            {
                this.nodeKey = key;
                this.rightNode = rightNode;
                this.leftNode = leftNode;
                this.parentNode = null;
            }

            public RedBlackNode(TKey key) : this(key, null, null) { }
            

        }

        public enum NodeColor
        {
           RED = 0,
           BLACK = 1
        }

        public enum Direction
        {
           LEFT = 0,
           RIGHT = 1
        }
    }
}
