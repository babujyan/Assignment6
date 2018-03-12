﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary
{
    public class RedBlackTree<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
    {
        private RedBlackNode rootNode;

        public TKey RootNode
        {
            get
            {
                return this.rootNode.NodeKey;
            }
        }

        private int nodeCount;

        public int NodeCount
        {
            get
            {
                return this.nodeCount;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this.rootNode == null);
            }
        }

        private List<KeyValuePair<TKey, TValue>> arr;




        /// <summary>
        /// 
        /// </summary>
        public RedBlackTree()
        {
            nodeCount = 0;
            rootNode = null;
        }

        /// <summary>
        /// 
        /// </summary>
        class RedBlackNode
        {
            private readonly TValue nodeValue;
            private readonly TKey nodeKey;


            private NodeColor color;
            private RedBlackNode parentNode;
            private RedBlackNode leftNode;
            private RedBlackNode rightNode;

            /// <summary>
            /// 
            /// </summary>
            public TKey NodeKey
            {
                get { return this.nodeKey; }
            }

            /// <summary>
            /// 
            /// </summary>
            public TValue NodeValue
            {
                get { return this.nodeValue; }
            }

            /// <summary>
            /// 
            /// </summary>
            public RedBlackNode Grandparent
            {
                get
                {
                    if (parentNode == null)
                    {
                        return null;
                    }
                    return this.parentNode.parentNode;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public RedBlackNode Sibling
            {
                get
                {
                    
                    if (this.parentNode == null)
                    {
                        return null; // No parent means no sibling
                    }
                    if (this.parentNode.leftNode != null)
                    {
                        if (this.nodeKey.CompareTo(this.parentNode.leftNode.nodeKey) == 0)
                        {
                            return this.parentNode.rightNode;
                        }
                        else
                        {
                            return this.parentNode.leftNode;
                        }
                    }
                    else
                    {
                        return this.parentNode.leftNode;
                    }
                }
            }

            /// <summary>
            /// /
            /// </summary>
            public RedBlackNode Uncle
            {
                get
                {
                    if (this.Grandparent == null)
                    {
                        return null;
                    }

                    return this.parentNode.Sibling;
                }
            }

            public RedBlackNode ParentNode
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

            public RedBlackNode LeftNode
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
            public RedBlackNode RightNode
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

            public NodeColor Color
            {
                get
                {
                    return this.color;
                }
                set
                {
                    this.color = value;
                }
            }

            public Direction ParentDirection
            {
                get
                {
                    if (parentNode == null || NodeKey.CompareTo(ParentNode.NodeKey) > 0)
                    {
                        return Direction.LEFT;
                    }

                    return Direction.RIGHT;
                }
            }

            public bool IsRoot
            {
                get { return (this.parentNode == null); }
            }

            public bool IsLeaf
            {
                get { return (this.leftNode == null && this.rightNode == null); }
            }

            public RedBlackNode(TKey key, TValue value, RedBlackNode rightNode, RedBlackNode leftNode)
            {
                this.nodeKey = key;
                this.nodeValue = value;
                this.rightNode = rightNode;
                this.leftNode = leftNode;
                this.parentNode = null;
            }

            public RedBlackNode(TKey key, TValue value) : this(key, value, null, null) { }

            public void RotateLeft()
            {
                if (this.rightNode != null)
                {
                    if (this.parentNode != null)
                    {
                        if (this.Sibling != null)
                        {
                            if (this.Sibling == this.parentNode.leftNode)// this means that "this" is right node.
                            {
                                var temp = this.rightNode;
                                while (temp.leftNode != null)
                                {
                                    temp = temp.leftNode;
                                }

                                this.rightNode.parentNode = this.parentNode;
                                this.parentNode.rightNode = this.rightNode;

                                temp.leftNode = this;
                                temp.leftNode.rightNode = null;
                                temp.leftNode.parentNode = temp;


                            }
                            else//left node
                            {
                                var temp = this.rightNode;
                                while (temp.leftNode != null)
                                {
                                    temp = temp.leftNode;
                                }

                                this.rightNode.parentNode = this.parentNode;
                                this.parentNode.leftNode = this.rightNode;

                                temp.leftNode = this;
                                temp.leftNode.rightNode = null;
                                temp.leftNode.parentNode = temp;

                            }
                        }
                        else if(this.parentNode.leftNode == null) // right
                        {
                            var temp = this.rightNode;
                            while (temp.leftNode != null)
                            {
                                temp = temp.leftNode;
                            }

                            this.rightNode.parentNode = this.parentNode;
                            this.parentNode.rightNode = this.rightNode;

                            temp.leftNode = this;
                            temp.leftNode.rightNode = null;
                            temp.leftNode.parentNode = temp;
                                                        
                        }
                        else
                        {
                            var temp = this.rightNode;
                            while (temp.leftNode != null)
                            {
                                temp = temp.leftNode;
                            }

                            this.rightNode.parentNode = this.parentNode;
                            this.parentNode.leftNode = this.rightNode;

                            temp.leftNode = this;
                            temp.leftNode.rightNode = null;
                            temp.leftNode.parentNode = temp;
                        }
                    }
                    else
                    {
                        var temp = this.rightNode;
                        while (temp.leftNode != null)
                        {
                            temp = temp.leftNode;
                        }

                        this.rightNode.parentNode = null;

                        temp.leftNode = this;
                        temp.leftNode.rightNode = null;
                        temp.leftNode.parentNode = temp;
                        this.parentNode = temp;
                        
                    }
                }
                else
                {
                    //throw new Exception("Can not Rotate left.");
                }
            }

            public void RotateRight()
            {
                if (this.leftNode != null)
                {
                    if (this.parentNode != null)
                    {
                        if (this.Sibling != null)
                        {

                            if (this.Sibling == this.parentNode.leftNode)// this means that "this" is right node.
                            {
                                var temp = this.leftNode;
                                while (temp.rightNode != null)
                                {
                                    temp = temp.rightNode;
                                }

                                this.leftNode.parentNode = this.parentNode;
                                this.parentNode.rightNode = this.leftNode;

                                temp.rightNode = this;
                                temp.rightNode.leftNode = null;
                                temp.rightNode.parentNode = temp;
                                

                            }
                            else//left node
                            {
                                var temp = this.leftNode;
                                while (temp.rightNode != null)
                                {
                                    temp = temp.rightNode;
                                }

                                this.leftNode.parentNode = this.parentNode;
                                this.parentNode.leftNode = this.leftNode;

                                temp.rightNode = this;
                                temp.rightNode.leftNode = null;
                                temp.rightNode.parentNode = temp;
                                
                            }
                        }
                        else if(this.parentNode.leftNode == null) //right
                        {
                            var temp = this.leftNode; 
                            while (temp.rightNode != null)
                            {
                                temp = temp.rightNode;
                            }

                            this.leftNode.parentNode = this.parentNode;
                            this.parentNode.rightNode = this.leftNode;

                            temp.rightNode = this;
                            temp.rightNode.leftNode = null;
                            temp.rightNode.parentNode = temp;

                        }
                        else
                        {
                            var temp = this.leftNode;
                            while (temp.rightNode != null)
                            {
                                temp = temp.rightNode;
                            }

                            this.leftNode.parentNode = this.parentNode;
                            this.parentNode.leftNode = this.leftNode;

                            temp.rightNode = this;
                            temp.rightNode.leftNode = null;
                            temp.rightNode.parentNode = temp;
                        }
                    }
                    else
                    {
                        var temp = this.leftNode;
                        while (temp.rightNode != null)
                        {
                            temp = temp.rightNode;
                        }

                        this.leftNode.parentNode = null;
                       
                        temp.rightNode = this;
                        temp.rightNode.leftNode = null;
                        temp.rightNode.parentNode = temp;
                        
                    }
                }
                else
                {
                   // throw new Exception("Can not Rotate Right.");
                }
            }

            public void RepairTree()
            {
                if (this.parentNode == null)
                {
                    this.color = NodeColor.BLACK;
                }
                else if (this.parentNode.Color != NodeColor.BLACK)
                {
                    if (this.Uncle.color == NodeColor.RED)//What if Uncle == null
                    {
                        this.parentNode.color = NodeColor.BLACK;
                        this.Uncle.color = NodeColor.BLACK;
                        this.Grandparent.color = NodeColor.RED;
                        this.Grandparent.RepairTree();
                    }
                    else
                    {

                        if (this == this.Grandparent.leftNode.rightNode)
                        {
                            this.parentNode.RotateLeft();
                            this.leftNode = this.leftNode.leftNode;
                            this.rightNode = this.leftNode.rightNode;
                            this.parentNode = this.leftNode.parentNode;
                            this.Color = this.leftNode.Color;
                        }
                        else if (this == this.Grandparent.rightNode.leftNode)
                        {
                            this.parentNode.RotateRight();
                            this.leftNode = this.rightNode.leftNode;
                            this.rightNode = this.rightNode.rightNode;
                            this.parentNode = this.rightNode.parentNode;
                            this.Color = this.rightNode.Color;
                        }


                        //case2
                        if (this == parentNode.leftNode)
                        {
                            this.Grandparent.RotateRight();
                        }
                        else
                        {
                            this.Grandparent.RotateLeft();
                        }

                        this.parentNode.color = NodeColor.BLACK;
                        this.Grandparent.color = NodeColor.RED; //dont understand what to do 
                    }                 
                }
            }
        }






        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count
        {
            get
            {
                return nodeCount;
            }
        }



        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => throw new NotImplementedException();



        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }









        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            if (this.IsEmpty)
            {
                return false;
            }
            var temp = this.rootNode;

            
            while (temp != null)
            {
                switch (key.CompareTo(temp.NodeKey))
                {
                    case -1:
                        temp = temp.LeftNode;
                        break;
                    case 1:
                        temp = temp.RightNode;
                        break;
                    default:
                        return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            RedBlackNode temp = this.rootNode;

            while (temp != null)
            {
                switch (key.CompareTo(temp.NodeKey))
                {
                    case -1:
                        temp = temp.LeftNode;
                        break;
                    case 1:
                        temp = temp.RightNode;
                        break;
                    default:
                        {
                            if(temp.IsLeaf)
                            {
                                if(temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = null;
                                    temp.ParentNode.RepairTree();
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                temp.ParentNode.RepairTree();
                                return true;
                            }
                            else if(temp.LeftNode == null)
                            {
                                temp.RightNode.ParentNode = temp.ParentNode;
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = temp.RightNode;
                                    temp.ParentNode.RepairTree();
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                temp.ParentNode.RepairTree();
                                return true;
                            }
                            else if(temp.RightNode == null)
                            {
                                temp.LeftNode.ParentNode = temp.ParentNode;
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = temp.LeftNode;
                                    temp.ParentNode.RepairTree();
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                temp.ParentNode.RepairTree();
                                return true;
                            }
                            else
                            {
                                temp.RotateLeft();
                                temp.ParentNode.LeftNode = temp.LeftNode;
                                temp.LeftNode.ParentNode = temp.ParentNode;
                                //temp.ParentNode.RepairTree();
                                return true;
                            }
                        }
                        
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            RedBlackNode temp = this.rootNode;

            while (temp != null)
            {
                switch (item.Key.CompareTo(temp.NodeKey))
                {
                    case -1:
                        temp = temp.LeftNode;
                        break;
                    case 1:
                        temp = temp.RightNode;
                        break;
                    default:
                        { 
                            if (temp.NodeValue.CompareTo(item.Value) == 0)
                            {
                                if (temp.IsLeaf)
                                {
                                    if (temp.ParentNode.LeftNode == temp)
                                    {
                                        temp.ParentNode.LeftNode = null;
                                        temp.ParentNode.RepairTree();
                                        return true;
                                    }
                                    temp.ParentNode.RightNode = null;
                                    temp.ParentNode.RepairTree();
                                    return true;
                                }
                                else if (temp.LeftNode == null)
                                {
                                    temp.RightNode.ParentNode = temp.ParentNode;
                                    if (temp.ParentNode.LeftNode == temp)
                                    {
                                        temp.ParentNode.LeftNode = temp.RightNode;
                                        temp.ParentNode.RepairTree();
                                        return true;
                                    }
                                    temp.ParentNode.RightNode = null;
                                    temp.ParentNode.RepairTree();
                                    return true;
                                }
                                else if (temp.RightNode == null)
                                {
                                    temp.LeftNode.ParentNode = temp.ParentNode;
                                    if (temp.ParentNode.LeftNode == temp)
                                    {
                                        temp.ParentNode.LeftNode = temp.LeftNode;
                                        temp.ParentNode.RepairTree();
                                        return true;
                                    }
                                    temp.ParentNode.RightNode = null;
                                    temp.ParentNode.RepairTree();
                                    return true;
                                }
                                else
                                {
                                    temp.RotateLeft();
                                    temp.ParentNode.LeftNode = temp.LeftNode;
                                    temp.LeftNode.ParentNode = temp.ParentNode;
                                    //temp.ParentNode.RepairTree();
                                    return true;
                                }
                            }
                            else return false;
                        }

                }
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            RedBlackNode newNode = new RedBlackNode(key, value);

            if (this.rootNode == null)
            {
                this.rootNode = newNode;
                this.nodeCount = 1;
                this.rootNode.Color = NodeColor.BLACK;
            }
            else
            {
                RecursiveAdd(newNode, this.rootNode);
            }

            newNode.RepairTree();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="rootNode"></param>
        private static void RecursiveAdd(RedBlackNode newNode, RedBlackNode rootNode)
        {
            if(rootNode == null)
            {
                throw new Exception("referenccce is null");
            }
            else if(newNode.NodeKey.CompareTo(rootNode.NodeKey) == -1)// new node is greater then root node
            {
                if (rootNode.RightNode != null)
                {
                    RecursiveAdd(newNode, rootNode.RightNode);
                    return;
                }
                else
                {
                    rootNode.RightNode = newNode;
                }
            }
            else
            {
                if (rootNode.LeftNode != null)
                {
                    RecursiveAdd(newNode, rootNode.LeftNode);
                    return;
                }
                else
                {
                    rootNode.LeftNode = newNode;
                }
            }

            newNode.ParentNode = rootNode;
            newNode.Color = NodeColor.RED;
        }
                
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this.nodeCount = 0;
            RecursiveClear(rootNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private static void RecursiveClear(RedBlackNode node)
        {
            if(node != null)
            {
                RecursiveClear(node.LeftNode);
                RecursiveClear(node.RightNode);
                node = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            value = this.rootNode.NodeValue;
            
            if (this.IsEmpty)
            {
                return false;
            }

            var temp = this.rootNode;

            while (temp != null)
            {
                switch (key.CompareTo(temp.NodeKey))
                {
                    case -1:
                        temp = temp.LeftNode;
                        break;
                    case 1:
                        temp = temp.RightNode;
                        break;
                    default:
                        value = temp.NodeValue;
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (this.IsEmpty)
            {
                return false;
            }
            var temp = this.rootNode;

            while (temp != null)
            {
                switch (item.Key.CompareTo(temp.NodeKey))
                {
                    case -1:
                        temp = temp.LeftNode;
                        break;
                    case 1:
                        temp = temp.RightNode;
                        break;
                    default:
                        if(item.Value.CompareTo(temp.NodeValue) == 0)
                        {
                            return true;
                        }
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            RedBlackNode temp = this.rootNode;
            int index = arrayIndex;
            AddToArr(ref index, temp, array);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="t"></param>
        /// <param name="array"></param>
        private void AddToArr(ref int i, RedBlackNode t , KeyValuePair<TKey, TValue>[] array)
        {
            if (t == null)
            {
                return;
            }

            array[i] = new KeyValuePair<TKey, TValue>(t.NodeKey, t.NodeValue);
            i++;
            AddToArr(ref i, t.LeftNode, array);
            AddToArr(ref i, t.RightNode, array);
        }



        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public enum NodeColor
        {
            RED = 0,
            BLACK = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum Direction
        {
            LEFT = 0,
            RIGHT = 1
        }
    }
}