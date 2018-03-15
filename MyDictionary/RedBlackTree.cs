using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary
{
    public class RedBlackTree<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerator<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey> where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Referens to Root node.
        /// </summary>
        private RedBlackNode rootNode;

        /// <summary>
        /// Current node.
        /// </summary>
        private RedBlackNode current;

        /// <summary>
        /// Has been modified or not.
        /// </summary>
        private bool isModfied;

        /// <summary>
        ///  Gets the root of the tree.
        /// </summary>
        public TKey RootNode
        {
            get
            {
                return this.rootNode.NodeKey;
            }
        }

        /// <summary>
        /// Number of nodes in tree.
        /// </summary>
        private int nodeCount;

        /// <summary>
        ///  Gets the number of elements contained in the tree.
        /// </summary>        
        public int Count
        {
            get
            {
                return nodeCount;
            }
        }

        /// <summary>
        /// Is tree empty or not.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (this.rootNode == null);
            }
        }

        /// <summary>
        /// Resets the currnet.(For Enumerator)
        /// </summary>
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
        /// Parametrless CTor.
        /// </summary>
        public RedBlackTree()
        {
            nodeCount = 0;
            rootNode = null;
            isModfied = false;
            this.Reset();

        }

        /// <summary>
        /// Type for Node.
        /// </summary>
        class RedBlackNode
        {
            /// <summary>
            /// Node Value.
            /// </summary>
            private TValue nodeValue;

            /// <summary>
            /// Node Key.
            /// </summary>
            private readonly TKey nodeKey;

            /// <summary>
            /// NOde color.
            /// </summary>
            private NodeColor color;

            /// <summary>
            /// Parent node.
            /// </summary>
            private RedBlackNode parentNode;

            /// <summary>
            /// Left node.
            /// </summary>
            private RedBlackNode leftNode;

            /// <summary>
            /// Right node.
            /// </summary>
            private RedBlackNode rightNode;

            /// <summary>
            /// Gets node Key.
            /// </summary>
            public TKey NodeKey
            {
                get { return this.nodeKey; }
            }

            /// <summary>
            /// Gets and sets node value.
            /// </summary>
            public TValue NodeValue
            {
                get { return this.nodeValue; }
                set { nodeValue = value; }
            }

            /// <summary>
            /// Grandparent node.
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
            /// Sibling node.
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
            /// UNcle node.
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

            /// <summary>
            /// Gets and sets the Parent node.
            /// </summary>
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
            
            /// <summary>
            /// Gets and sets the Left node.
            /// </summary>
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

            /// <summary>
            /// Gets and sets the right node.
            /// </summary>
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

            /// <summary>
            /// Gets and sets the node color.
            /// </summary>
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

            /// <summary>
            /// Is root or not.
            /// </summary>
            public bool IsRoot
            {
                get { return (this.parentNode == null); }
            }

            /// <summary>
            /// Is leaf or not.
            /// </summary>
            public bool IsLeaf
            {
                get { return (this.leftNode == null && this.rightNode == null); }
            }

            /// <summary>
            /// CTor for node.
            /// </summary>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            /// <param name="rightNode">Right node.</param>
            /// <param name="leftNode">Left node.</param>
            public RedBlackNode(TKey key, TValue value, RedBlackNode rightNode, RedBlackNode leftNode)
            {
                this.nodeKey = key;
                this.nodeValue = value;
                this.rightNode = rightNode;
                this.leftNode = leftNode;
                this.parentNode = null;
                this.color = NodeColor.RED;
            }

            /// <summary>
            /// CTor for node.
            /// </summary>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            public RedBlackNode(TKey key, TValue value) : this(key, value, null, null) { }


            /// <summary>
            /// Rotates node left.
            /// </summary>
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

            /// <summary>
            /// Rotates node right.
            /// </summary>
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
                
        /// <summary>
        /// Gets a value indicating whether the tree is read-only.
        /// </summary>
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

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

        /// <summary>
        /// Returns Collection of Keys.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> list = new List<TKey>();
                this.Reset();
                foreach(var i in this)
                {
                   
                    list.Add(i.Key);
                }
                return list;
            }
        }

        /// <summary>
        /// Returns collection of Values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> list = new List<TValue>();
                this.Reset();
                foreach (var i in this)
                {
                    list.Add(i.Value);
                }
                return list;
            }
        }

        /// <summary>
        /// Indexator for tree.
        /// </summary>
        /// <param name="key">Index of the iteam.</param>
        /// <returns>Value of the iteam. </returns>
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
        /// Retrns true if tree contains the key.
        /// </summary>
        /// <param name="key">Key of the element.</param>
        /// <returns>Wether or not tre contains the key.</returns>
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
        /// Removes the elemet from tree.
        /// </summary>
        /// <param name="key">Key of the elemet.</param>
        /// <returns>True if removed.</returns>
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
                            if (temp.IsLeaf)
                            {
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = null;
                                    this.RepairTree(temp.ParentNode);
                                    nodeCount--;
                                    isModfied = true;
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                            else if (temp.LeftNode == null)
                            {
                                temp.RightNode.ParentNode = temp.ParentNode;
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = temp.RightNode;
                                    this.RepairTree(temp.ParentNode);
                                    nodeCount--;
                                    isModfied = true;
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                            else if (temp.RightNode == null)
                            {
                                temp.LeftNode.ParentNode = temp.ParentNode;
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = temp.LeftNode;
                                    this.RepairTree(temp.ParentNode);
                                    nodeCount--;
                                    isModfied = true;
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                            else
                            {
                                temp.RotateLeft();
                                temp.ParentNode.LeftNode = temp.LeftNode;
                                temp.LeftNode.ParentNode = temp.ParentNode;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                        }
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the elemet from tree.
        /// </summary>
        /// <param name="item">Key Value pair of the elemet.</param>
        /// <returns>True if removed.</returns> 
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
                            if (temp.IsLeaf)
                            {
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = null;
                                    this.RepairTree(temp.ParentNode);
                                    nodeCount--;
                                    isModfied = true;
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                            else if (temp.LeftNode == null)
                            {
                                temp.RightNode.ParentNode = temp.ParentNode;
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = temp.RightNode;
                                    this.RepairTree(temp.ParentNode);
                                    nodeCount--;
                                    isModfied = true;
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                            else if (temp.RightNode == null)
                            {
                                temp.LeftNode.ParentNode = temp.ParentNode;
                                if (temp.ParentNode.LeftNode == temp)
                                {
                                    temp.ParentNode.LeftNode = temp.LeftNode;
                                    this.RepairTree(temp.ParentNode);
                                    nodeCount--;
                                    isModfied = true;
                                    return true;
                                }
                                temp.ParentNode.RightNode = null;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                            else
                            {
                                temp.RotateLeft();
                                temp.ParentNode.LeftNode = temp.LeftNode;
                                temp.LeftNode.ParentNode = temp.ParentNode;
                                this.RepairTree(temp.ParentNode);
                                nodeCount--;
                                isModfied = true;
                                return true;
                            }
                        }
                }
            }
            return false;
        }
        
        /// <summary>
        /// Adds a node.
        /// </summary>
        /// <param name="key">Key of the node.</param>
        /// <param name="value">Value of the node. </param>
        public void Add(TKey key, TValue value)
        {
            RedBlackNode newNode = new RedBlackNode(key, value);
            
            if(this.rootNode == null)
            {
                this.rootNode = newNode;
                this.rootNode.Color = NodeColor.BLACK;
                nodeCount++;
                isModfied = true;
                return;
            }

            RecursiveAdd(newNode, this.rootNode);

            this.RepairTree(newNode);

            this.nodeCount++;
            isModfied = true;
            this.rootNode.Color = NodeColor.BLACK;
            return;           
        }

        /// <summary>
        /// Adds a node.
        /// </summary>
        /// <param name="item">Key Value pair of the node.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Adds a node.
        /// </summary>
        /// <param name="newNode">New NOde which should be added.</param>
        /// <param name="rootNode">Root node of subtree.</param>
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

        /// <summary>
        /// Corects tree.
        /// </summary>
        /// <param name="node">Start fom this node.</param>
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
        /// Clear the tree.
        /// </summary>
        public void Clear()
        {
            this.nodeCount = 0;
            RecursiveClear(rootNode);
        }

        /// <summary>
        /// Recursive clears the tree.
        /// </summary>
        /// <param name="node">from this node.</param>
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
        /// Try Get Value of the NOde.
        /// </summary>
        /// <param name="key">Key of the Node.</param>
        /// <param name="value">Value of the Node.</param>
        /// <returns>True if got.</returns>
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
        /// If tree contains the node.
        /// </summary>
        /// <param name="item">Node</param>
        /// <returns>True if contains.</returns>
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
        /// Copy to array.
        /// </summary>
        /// <param name="array">Array.</param>
        /// <param name="arrayIndex">From which index.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            RedBlackNode temp = this.rootNode;
            int index = arrayIndex;
            AddToArr(ref index, temp, array);

        }

        /// <summary>
        /// Copy subtree into array.
        /// </summary>
        /// <param name="i">Index of the array.</param>
        /// <param name="t">Root of the subtree.</param>
        /// <param name="array">Array.</param>
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
        
        /// <summary>
        /// Move to next node.
        /// </summary>
        /// <returns>True if moved.</returns>
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
        
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            
            this.isModfied = false;
            this.Reset();
            return (IEnumerator<KeyValuePair<TKey, TValue>>)this;
        }
              
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            this.isModfied = false;
            return (IEnumerator)this;
        }

        /// <summary>
        /// Enum for NOde color.
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