﻿using System;
using System.Collections.Generic;

namespace VDS.Common.Filters
{
    /// <summary>
    /// Abstract implementation of a fast bloom filter using the methodology outlined in <a href="http://citeseer.ist.psu.edu/viewdoc/download;jsessionid=4060353E67A356EF9528D2C57C064F5A?doi=10.1.1.152.579&rep=rep1&type=pdf">Less Hashing, Same Performance: Building a Better Bloom Filter</a>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// This implementation works on a technique described in the literature which shows that given two hash functions it is possible to simulate any number of hash functions by performing simple arithmetic combinations of the two hash functions outputs.  In practise this means that the hashing used by the bloom filter is significantly faster because it needs only calculate two hash functions for any given item and then can compute the necessary number of hash values by simple arithmetic operations.
    /// </remarks>
    public abstract class BaseFastBloomFilter<T>
        : BaseBloomFilter<T>
    {
        private readonly Func<T, int> _h1, _h2;
        private readonly IBloomFilterParameters _parameters;

        /// <summary>
        /// Creates a new 
        /// </summary>
        /// <param name="storage">Bloom Filter Storage</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="h1">First hash function</param>
        /// <param name="h2">Second hash function</param>
        protected BaseFastBloomFilter(IBloomFilterStorage storage, IBloomFilterParameters parameters, Func<T, int> h1, Func<T, int> h2)
            : base(storage)
        {
            if (parameters == null) throw new ArgumentNullException("parameters", "Paramaeters cannot be null");
            if (h1 == null) throw new ArgumentException("Hash functions cannot be null", "h1");
            if (h2 == null) throw new ArgumentException("Hash functions cannot be null", "h2");
            if (parameters.NumberOfBits <= parameters.NumberOfHashFunctions) throw new ArgumentException("Number of bits must be bigger than the number of hash functions", "parameters");

            this._parameters = parameters;
            this.NumberOfBits = parameters.NumberOfBits;
            this._h1 = h1;
            this._h2 = h2;
        }

        public override int NumberOfHashFunctions { get { return this._parameters.NumberOfHashFunctions; } }

        protected override IEnumerable<int> GetBitIndices(T item)
        {
            int a = this._h1(item);
            int b = this._h2(item);

            int[] bits = new int[this._parameters.NumberOfHashFunctions];
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = Math.Abs(a + (i*b)) % this.NumberOfBits;
            }
            return bits;
        }
    }
}