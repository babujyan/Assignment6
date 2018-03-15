using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary
{
    public class RedBlackTree<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerator<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
    {
        private RedBlackNode rootNode;

        private RedBlackNode current;

        private bool isModfied;

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
                
        public void Reset()
        {
            if (!isModfied)
            {
                if (this.rootNode == null)
                {
                    return;
                }

                this.current = null;
            }
            else
                throw new Exception("dbddhsbd");
        }

        /// <summary>
        /// 
        /// </summary>
        public RedBlackTree()
        {
            nodeCount = 0;
            rootNode = null;
            isModfied = false;
            this.Reset();

        }

        /// <summary>
        /// 
        /// </summary>
        class RedBlackNode
        {
            private TValue nodeValue;
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
                set { nodeValue = value; }
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
                this.color = NodeColor.RED;
            }

            public RedBlackNode(TKey key, TValue value) : this(key, value, null, null) { }

            public void RotateLeft()
            {
                if (this.rightNode != null)
                {
                    RedBlackNode node = this.rightNode;

                    this.rightNode = node.leftNode;
                    if (node.leftNode != null)
                    {
                        node.leftNode.parentNode = this;
                    }
                    node.leftNode = this;
                    node.parentNode = this.parentNode;
                    if (this.parentNode != null)
                    {
                        if (this == this.parentNode.leftNode)
                        {
                            this.parentNode.leftNode = node;
                        }
                        else
                        {
                            this.parentNode.rightNode = node;
                        }
                    }
                    node.leftNode.color = NodeColor.RED;
                    //node.rightNode.color = NodeColor.RED;
                    node.color = NodeColor.BLACK;

                    this.parentNode = node;                    
                }
            }


            public void RotateRight()
            {
                if (this.leftNode != null)
                {
                    RedBlackNode node = this.leftNode;

                    this.leftNode = node.rightNode;
                    if(node.rightNode != null)
                    {
                        node.rightNode.parentNode = this;
                    }
                    node.rightNode = this;
                    node.parentNode = this.parentNode;
                    if(this.parentNode != null)
                    {
                        if(this == this.parentNode.leftNode)
                        {
                            this.parentNode.leftNode = node;
                        }
                        else
                        {
                            this.parentNode.rightNode = node;
                        }
                    }
                    //node.leftNode.color = NodeColor.RED;
                    node.rightNode.color = NodeColor.RED;
                    node.color = NodeColor.BLACK;
                    this.parentNode = node;
                }


                return;                
            }           
        }


       

        public int Count
        {
            get
            {
                return nodeCount;
            }
        }
        


        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => throw new NotImplementedException();

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
        {

            /// <summary>
            /// Returns: The element in the collection at the current position of the enumerator.
            /// </summary>
            get
            {
                return new KeyValuePair<TKey, TValue>(this.current.NodeKey, this.current.NodeValue);
            }
        }


        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        object IEnumerator.Current
        {
            ///<summary>
            ///Returns: The current element in the collection.
            ///</summary>
            get
            {
                return (object)current;
            }
        }

        public ICollection<TKey> Keys  => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (this.TryGetValue(key, out value))
                {
                    return value;
                }
                throw new Exception("Does not contain this key");
            }
            set
            {                
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
                            temp.NodeValue = value;
                            return;
                    }
                }

                throw new Exception("Does not contain this key");
            }
        }

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
            //RedBlackNode temp = this.rootNode;

            //while (temp != null)
            //{
            //    switch (key.CompareTo(temp.NodeKey))
            //    {
            //        case -1:
            //            temp = temp.LeftNode;
            //            break;
            //        case 1:
            //            temp = temp.RightNode;
            //            break;
            //        default:
            //            {
            //                if(temp.IsLeaf)
            //                {
            //                    if(temp.ParentNode.LeftNode == temp)
            //                    {
            //                        temp.ParentNode.LeftNode = null;
            //                        temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                    temp.ParentNode.RightNode = null;
            //                    temp.ParentNode.RepairTree();
            //                    return true;
            //                }
            //                else if(temp.LeftNode == null)
            //                {
            //                    temp.RightNode.ParentNode = temp.ParentNode;
            //                    if (temp.ParentNode.LeftNode == temp)
            //                    {
            //                        temp.ParentNode.LeftNode = temp.RightNode;
            //                        temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                    temp.ParentNode.RightNode = null;
            //                    temp.ParentNode.RepairTree();
            //                    return true;
            //                }
            //                else if(temp.RightNode == null)
            //                {
            //                    temp.LeftNode.ParentNode = temp.ParentNode;
            //                    if (temp.ParentNode.LeftNode == temp)
            //                    {
            //                        temp.ParentNode.LeftNode = temp.LeftNode;
            //                        temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                    temp.ParentNode.RightNode = null;
            //                    temp.ParentNode.RepairTree();
            //                    return true;
            //                }
            //                else
            //                {
            //                    temp.RotateLeft();
            //                    temp.ParentNode.LeftNode = temp.LeftNode;
            //                    temp.LeftNode.ParentNode = temp.ParentNode;
            //                    //temp.ParentNode.RepairTree();
            //                    return true;
            //                }
            //            }
                        
            //    }
            //}
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            //RedBlackNode temp = this.rootNode;

            //while (temp != null)
            //{
            //    switch (item.Key.CompareTo(temp.NodeKey))
            //    {
            //        case -1:
            //            temp = temp.LeftNode;
            //            break;
            //        case 1:
            //            temp = temp.RightNode;
            //            break;
            //        default:
            //            { 
            //                if (temp.NodeValue.CompareTo(item.Value) == 0)
            //                {
            //                    if (temp.IsLeaf)
            //                    {
            //                        if (temp.ParentNode.LeftNode == temp)
            //                        {
            //                            temp.ParentNode.LeftNode = null;
            //                            temp.ParentNode.RepairTree();
            //                            return true;
            //                        }
            //                        temp.ParentNode.RightNode = null;
            //                        temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                    else if (temp.LeftNode == null)
            //                    {
            //                        temp.RightNode.ParentNode = temp.ParentNode;
            //                        if (temp.ParentNode.LeftNode == temp)
            //                        {
            //                            temp.ParentNode.LeftNode = temp.RightNode;
            //                            temp.ParentNode.RepairTree();
            //                            return true;
            //                        }
            //                        temp.ParentNode.RightNode = null;
            //                        temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                    else if (temp.RightNode == null)
            //                    {
            //                        temp.LeftNode.ParentNode = temp.ParentNode;
            //                        if (temp.ParentNode.LeftNode == temp)
            //                        {
            //                            temp.ParentNode.LeftNode = temp.LeftNode;
            //                            temp.ParentNode.RepairTree();
            //                            return true;
            //                        }
            //                        temp.ParentNode.RightNode = null;
            //                        temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                    else
            //                    {
            //                        temp.RotateLeft();
            //                        temp.ParentNode.LeftNode = temp.LeftNode;
            //                        temp.LeftNode.ParentNode = temp.ParentNode;
            //                        //temp.ParentNode.RepairTree();
            //                        return true;
            //                    }
            //                }
            //                else return false;
            //            }

            //    }
            //}
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
            
            if(this.rootNode == null)
            {
                this.rootNode = newNode;
                this.rootNode.Color = NodeColor.BLACK;
                return;
            }

            RecursiveAdd(newNode, this.rootNode);

            this.RepairTree(newNode);

            this.nodeCount++;
            this.rootNode.Color = NodeColor.BLACK;
            return;           
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
            if(newNode.NodeKey.CompareTo(rootNode.NodeKey) == 0)
            {
                throw new ArgumentException("An item with the same key has already been added.");
            }

            if (newNode.NodeKey.CompareTo(rootNode.NodeKey) < 0)
            {
                if (rootNode.LeftNode != null)
                {
                    RecursiveAdd(newNode, rootNode.LeftNode);
                    return;
                }
                
                rootNode.LeftNode = newNode;
                rootNode.LeftNode.ParentNode = rootNode;
                return;
            }

            if (rootNode.RightNode != null)
            {
                RecursiveAdd(newNode, rootNode.RightNode);
                return;
            }
                
            rootNode.RightNode = newNode;
            rootNode.RightNode.ParentNode = rootNode;
            return;            
        }

        private void RepairTree(RedBlackNode node)
        {
            if(node == this.rootNode)
            {
                return;
            }
            //if (node.ParentNode == null)//case1
            //{
            //    this.rootNode = node;
            //    node.Color = NodeColor.BLACK;
            //}
            if (node.ParentNode.Color == NodeColor.BLACK)
            {
                return;
            }
            if (node.Uncle != null && node.Uncle.Color == NodeColor.RED)
            {
                //What if Uncle == null

                node.ParentNode.Color = NodeColor.BLACK;
                node.Uncle.Color = NodeColor.BLACK;
                node.Grandparent.Color = NodeColor.RED;
                RepairTree(node.Grandparent);
                return;

            }//case 3 
            else //case 4
            {
                RedBlackNode p = node.ParentNode;
                RedBlackNode g = node.Grandparent;

                if(p == g.RightNode)
                {
                    if(node == p.RightNode)
                    {
                        g.RotateLeft();
                        if (p.ParentNode == null)
                        {
                            this.rootNode = p;
                        }
                        return;
                    }
                    node.ParentNode.RotateRight();
                    node.ParentNode.RotateLeft();
                    if (node == null)
                    {
                        this.rootNode = node;
                    }
                    return;
                }
                if (node == p.LeftNode)
                {
                    g.RotateRight();
                    if (p.ParentNode == null)
                    {
                        this.rootNode = p;
                    }
                    return;
                }
                node.ParentNode.RotateLeft();
                node.ParentNode.RotateRight();

                if (node == null)
                {
                    this.rootNode = node;
                }
                return;



               
            }//case4            
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

        public bool MoveNext()
        {
            RedBlackNode temp;
            if (this.current != null)
            {
                if (current.RightNode == null)
                {
                    if (this.current.ParentNode == null)
                    {
                        return false;
                    }

                    temp = current;
                    while (temp.ParentNode != null && temp.ParentNode.RightNode == temp)
                    {
                        temp = temp.ParentNode;
                    }


                    if (temp.ParentNode != null)
                    {
                        this.current = temp.ParentNode;
                        return true;
                    }

                    this.current = temp;
                    this.MoveNext();

                    return false;
                }

                temp = current.RightNode;
                while (temp.LeftNode != null)
                {
                    temp = temp.LeftNode;
                }

                current = temp;
                return true;

            }

            temp = this.rootNode;
            while (temp.LeftNode != null)
            {
                temp = temp.LeftNode;
            }

            this.current = temp;
            return true;

        }

        

        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            
            this.isModfied = false;
            this.Reset();
            return (IEnumerator<KeyValuePair<TKey, TValue>>)this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            this.isModfied = false;
            return (IEnumerator)this;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum NodeColor
        {
            RED = 0,
            BLACK = 1
        }

        

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RedBlackTree() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}