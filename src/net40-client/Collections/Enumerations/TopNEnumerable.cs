/*
dotNetRDF is free and open source software licensed under the MIT License

-----------------------------------------------------------------------------

Copyright (c) 2009-2015 dotNetRDF Project (dotnetrdf-develop@lists.sf.net)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is furnished
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Collections.Generic;

namespace VDS.Common.Collections.Enumerations
{
    public class TopNEnumerable<T>
        : AbstractTopNEnumerable<T>
    {
        public TopNEnumerable(IEnumerable<T> enumerable, IComparer<T> comparer, long n)
            : base(enumerable, comparer, n) { }

        public override IEnumerator<T> GetEnumerator()
        {
            return new TopNEnumerator<T>(this.InnerEnumerable.GetEnumerator(), this.Comparer, this.N);
        }
    }

    public class TopNEnumerator<T>
        : AbstractTopNEnumerator<T>
    {
        public TopNEnumerator(IEnumerator<T> enumerator, IComparer<T> comparer, long n)
            : base(enumerator, comparer, n)
        {
            this.TopItems = new DuplicateSortedList<T>(comparer);
        }

        private DuplicateSortedList<T> TopItems { get; set; }

        protected override IEnumerator<T> BuildTopItems()
        {
            this.TopItems.Clear();
            while (this.InnerEnumerator.MoveNext())
            {
                this.TopItems.Add(this.InnerEnumerator.Current);
                if (this.TopItems.Count > this.N) this.TopItems.RemoveAt(this.TopItems.Count - 1);
            }
            return this.TopItems.GetEnumerator();
        }
    }
}
