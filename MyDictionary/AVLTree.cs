using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyDictionary
{
    public class AVLTree<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerator<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    {
       
        /// <summary>
        /// Referens to Root node.
        /// </summary>
        private AVLNode rootNode;

        /// <summary>
        /// Current node.
        /// </summary>
        private AVLNode current;

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
        public AVLTree()
        {
            nodeCount = 0;
            rootNode = null;
            isModfied = false;
            this.Reset();

        }

        /// <summary>
        /// Type for Node.
        /// </summary>
        class AVLNode
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
            /// Parent node.
            /// </summary>
            private AVLNode parentNode;

            /// <summary>
            /// Left node.
            /// </summary>
            private AVLNode leftNode;

            /// <summary>
            /// Right node.
            /// </summary>
            private AVLNode rightNode;

            

            public int BalanceFactor
            {
                get
                {
                    int right;
                    int left;
                    right = (this.rightNode == null) ? -1 : this.rightNode.Height;
                    left = (this.leftNode == null) ? -1 : this.leftNode.Height;
                    return (left - right);

                }
            }

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
            /// Gets and sets the Parent node.
            /// </summary>
            public AVLNode ParentNode
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
            public AVLNode LeftNode
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
            public AVLNode RightNode
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
            /// The Heght of tis node.
            /// </summary>
            public int Height
            {
                get
                {
                    if (IsLeaf)
                    {
                        return 1;
                    }

                    if (this.leftNode == null)
                    {
                        return (1 + this.rightNode.Height);
                    }

                    if (this.rightNode == null)
                    {
                        return (1 + this.LeftNode.Height);
                    }

                    if(this.leftNode.Height > this.rightNode.Height)
                    {
                        return (1 + this.LeftNode.Height);
                    }

                    return (1 + this.rightNode.Height);
                }
            }

            /// <summary>
            /// CTor for node.
            /// </summary>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            /// <param name="rightNode">Right node.</param>
            /// <param name="leftNode">Left node.</param>
            public AVLNode(TKey key, TValue value, AVLNode rightNode, AVLNode leftNode)
            {
                this.nodeKey = key;
                this.nodeValue = value;
                this.rightNode = rightNode;
                this.leftNode = leftNode;
                this.parentNode = null;
            }

            /// <summary>
            /// CTor for node.
            /// </summary>
            /// <param name="key">Key</param>
            /// <param name="value">Value</param>
            public AVLNode(TKey key, TValue value) : this(key, value, null, null) { }

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
                foreach (var i in this)
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
            AVLNode temp = this.rootNode;

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
                                this.RotateLeft(temp);
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
            AVLNode temp = this.rootNode;

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
                                this.RotateLeft(temp);
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
            AVLNode newNode = new AVLNode(key, value);

            if (this.rootNode == null)
            {
                this.rootNode = newNode;
                
                nodeCount++;
                isModfied = true;
                return;
            }

            RecursiveAdd(newNode, this.rootNode);

            this.RepairTree(newNode);

            this.nodeCount++;
            isModfied = true;
            
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
        private static void RecursiveAdd(AVLNode newNode, AVLNode rootNode)
        {
            if (newNode.NodeKey.CompareTo(rootNode.NodeKey) == 0)
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
        private void RepairTree(AVLNode node)
        {
            AVLNode temp = node;

            //if (node == this.rootNode)
            //{ 
            //    return;
            //}

            

            while (temp.BalanceFactor <= 1 && temp.BalanceFactor >= -1)
            {
                if (temp.ParentNode != null)
                    this.RepairTree(temp.ParentNode);
                return;
            }

            if(temp.BalanceFactor > 1)
            {
                if(temp.LeftNode.BalanceFactor > -1)
                {
                    this.RotateRight(temp);
                    RepairTree(temp);
                    return;
                }

                this.RotateLeft(temp.LeftNode);
                this.RotateRight(temp);
                return;
            }

            if(temp.RightNode.BalanceFactor < 1)
            {
                this.RotateLeft(temp);
                return;
            }

            this.RotateRight(temp.RightNode);
            this.RotateLeft(temp);
            return;                     
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
        private static void RecursiveClear(AVLNode node)
        {
            if (node != null)
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
                        if (item.Value.Equals(temp.NodeValue))
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
            AVLNode temp = this.rootNode;
            int index = arrayIndex;
            AddToArr(ref index, temp, array);

        }

        /// <summary>
        /// Copy subtree into array.
        /// </summary>
        /// <param name="i">Index of the array.</param>
        /// <param name="t">Root of the subtree.</param>
        /// <param name="array">Array.</param>
        private void AddToArr(ref int i, AVLNode t, KeyValuePair<TKey, TValue>[] array)
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
            AVLNode temp;
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
        /// Rotates node left.
        /// </summary>
        private void RotateLeft(AVLNode n)
        {
            if (n.RightNode != null)
            {
                AVLNode node = n.RightNode;

                n.RightNode = node.LeftNode;
                if (node.LeftNode != null)
                {
                    node.LeftNode.ParentNode = n;
                }
                node.LeftNode = n;
                node.ParentNode = n.ParentNode;
                if (n.ParentNode != null)
                {
                    if (n == n.ParentNode.LeftNode)
                    {
                        n.ParentNode.LeftNode = node;
                    }
                    else
                    {
                        n.ParentNode.RightNode = node;
                    }
                }

                if (n.ParentNode == null)
                {
                    this.rootNode = node;
                    n.ParentNode = node;
                }
                n.ParentNode = node;
            }
        }

        /// <summary>
        /// Rotates node right.
        /// </summary>
        private void RotateRight(AVLNode n)
        {
            if (n.LeftNode != null)
            {
                AVLNode node = n.LeftNode;

                n.LeftNode = node.RightNode;
                if (node.RightNode != null)
                {
                    node.RightNode.ParentNode = n;
                }
                node.RightNode = n;
                node.ParentNode = n.ParentNode;
                if (n.ParentNode != null)
                {
                    if (n == n.ParentNode.LeftNode)
                    {
                        n.ParentNode.LeftNode = node;
                    }
                    else
                    {
                        n.ParentNode.RightNode = node;
                    }
                }

                if (n.ParentNode == null)
                {
                    this.rootNode = node;
                    n.ParentNode = node;
                }

                n.ParentNode = node;
            }


            return;
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
        // ~AVLTree() {
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