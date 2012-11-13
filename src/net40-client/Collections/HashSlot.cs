/*
VDS.Common is licensed under the MIT License

Copyright (c) 2009-2012 Rob Vesse

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute,
sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VDS.Common.Collections
{
    /// <summary>
    /// Represents a slot in the Hash Table
    /// </summary>
    /// <typeparam name="T">Type of Values stored in the Slot</typeparam>
    /// <remarks>
    /// Hash Slots may contain duplicate values
    /// </remarks>
    [Obsolete("The HashTable class has known deficiencies in hash code collision behaviour and has been superceded by MultiDictionary which should be used instead, HashTable will be removed in subsequent releases", true)]
    class ListSlot<T> 
        : IHashSlot<T>
    {
        private List<T> _values;

        /// <summary>
        /// Creates a new Hash Slot which contains the given Value
        /// </summary>
        /// <param name="value">Value</param>
        public ListSlot(T value)
        {
            this._values = new List<T>(1);
            this._values.Add(value);
        }

        /// <summary>
        /// Creates a new Hash Slot which is an empty
        /// </summary>
        /// <param name="capacity">Initial Capacity of Slot</param>
        public ListSlot(int capacity)
        {
            this._values = new List<T>(capacity);
        }

        /// <summary>
        /// Creates a new Hash Slot which contains the given Value and has the given initial capacity
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="capacity">Initial Capacity of Slot</param>
        public ListSlot(T value, int capacity)
        {
            if (capacity >= 1)
            {
                this._values = new List<T>(capacity);
            }
            else
            {
                this._values = new List<T>();
            }
            this._values.Add(value);
        }

        /// <summary>
        /// Gets the Enumerator of the Values in the Slot
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._values.GetEnumerator();
        }

        /// <summary>
        /// Gets the Enumerator of the Values in the Slot
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._values.GetEnumerator();
        }

        /// <summary>
        /// Clears the contents of the Hash Slot
        /// </summary>
        public void Clear()
        {
            this._values.Clear();
        }

        /// <summary>
        /// Checks whether the Hash Slot contains a given Value
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return this._values.Contains(item);
        }

        /// <summary>
        /// Copies the contents of the Hash Slot to an array
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Index of the array at which to start copying data in</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (T value in this._values)
            {
                array[i] = value;
                i++;
            }
        }

        /// <summary>
        /// Gets the Count of Values in the Hash Slot
        /// </summary>
        public int Count
        {
            get
            {
                return this._values.Count;
            }
        }

        /// <summary>
        /// Returns that a Hash Slot is not read only
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Removes an Item from the Hash Slot
        /// </summary>
        /// <param name="item">Value to remove</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return this._values.Remove(item);
        }

        /// <summary>
        /// Adds an Item to the Hash Slot
        /// </summary>
        /// <param name="item">Value to add</param>
        public void Add(T item)
        {
            this._values.Add(item);
        }
    }

    [Obsolete("The HashTable class has known deficiencies in hash code collision behaviour and has been superceded by MultiDictionary which should be used instead, HashTable will be removed in subsequent releases", false)]
    class CompactSlot<T>
        : IHashSlot<T>
    {
        private LinkedList<T> _values;

        public CompactSlot()
        {
            this._values = new LinkedList<T>();
        }

        public CompactSlot(T value)
        {
            this._values = new LinkedList<T>();
            this._values.AddLast(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            this._values.AddLast(item);
        }

        public void Clear()
        {
            this._values.Clear();
        }

        public bool Contains(T item)
        {
            return this._values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this._values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return this._values.Count;
            }
        }

        public bool IsReadOnly
        {
            get 
            {
                return false;
            }
        }

        public bool Remove(T item)
        {
            return this._values.Remove(item);
        }
    }

#if !SILVERLIGHT

    [Obsolete("The HashTable class has known deficiencies in hash code collision behaviour and has been superceded by MultiDictionary which should be used instead, HashTable will be removed in subsequent releases", false)]
    class SetSlot<T>
        : IHashSlot<T>
    {
        private HashSet<T> _values;

        public SetSlot()
        {
            this._values = new HashSet<T>();
        }

        public SetSlot(T value)
        {
            this._values = new HashSet<T>();
            this._values.Add(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            this._values.Add(item);
        }

        public void Clear()
        {
            this._values.Clear();
        }

        public bool Contains(T item)
        {
            return this._values.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this._values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get 
            {
                return this._values.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(T item)
        {
            return this._values.Remove(item);
        }
    }

#endif
}
