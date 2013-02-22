﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VDS.Common.Tries
{
    [TestClass]
    public abstract class AbstractTrieContractTests
    {
        protected abstract ITrie<String, char, String> GetInstance();

        [TestMethod]
        public void TrieContractAdd1()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");

            Assert.AreEqual("a", trie["test"]);
        }

        [TestMethod]
        public void TrieContractAdd2()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");
            trie.Add("testing", "b");

            Assert.AreEqual("a", trie["test"]);
            Assert.AreEqual("b", trie["testing"]);
        }

        [TestMethod]
        public void TrieContractClear1()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");

            Assert.AreEqual("a", trie["test"]);

            trie.Clear();

            Assert.IsFalse(trie.ContainsKey("test"));
        }

        [TestMethod]
        public void TrieContractClear2()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");
            trie.Add("testing", "b");

            Assert.AreEqual("a", trie["test"]);
            Assert.AreEqual("b", trie["testing"]);

            trie.Clear();

            Assert.IsFalse(trie.ContainsKey("test"));
            Assert.IsFalse(trie.ContainsKey("testing"));
        }

        [TestMethod]
        public void TrieContractContains1()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");
            trie.Add("testing", "b");

            Assert.IsTrue(trie.ContainsKey("test"));
            Assert.IsTrue(trie.ContainsKey("testing"));
        }

        [TestMethod]
        public void TrieContractContains2()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            String key = "test";
            trie.Add(key, "a");

            Assert.IsTrue(trie.ContainsKey(key));
            for (int i = 1; i < key.Length; i++)
            {
                Assert.IsFalse(trie.ContainsKey(key.Substring(0, i)));
                Assert.IsTrue(trie.ContainsKey(key.Substring(0, i), false));
            }
        }

        [TestMethod]
        public void TrieContractRemove1()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");
            trie.Add("testing", "b");

            Assert.AreEqual("a", trie["test"]);
            Assert.AreEqual("b", trie["testing"]);

            trie.Remove("test");

            Assert.AreEqual("b", trie["testing"]);
            Assert.IsNotNull(trie.Find("test"));
        }

        [TestMethod]
        public void TrieContractRemove2()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");
            trie.Add("testing", "b");

            Assert.AreEqual("a", trie["test"]);
            Assert.AreEqual("b", trie["testing"]);

            trie.Remove("testing");

            Assert.AreEqual("a", trie["test"]);
            Assert.IsNull(trie.Find("testing"));
        }

        [TestMethod]
        public void TrieContractItemGet1()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");

            Assert.AreEqual("a", trie["test"]);
        }

        [TestMethod]
        public void TrieContractItemGet2()
        {
            ITrie<String, char, String> trie = this.GetInstance();
            trie.Add("test", "a");
            trie.Add("testing", "b");

            Assert.AreEqual("a", trie["test"]);
            Assert.AreEqual("b", trie["testing"]);
        }

        [TestMethod, ExpectedException(typeof(KeyNotFoundException))]
        public void TrieContractItemGet3()
        {
            ITrie<String, char, String> trie = this.GetInstance();

            Assert.AreEqual(null, trie["test"]);
        }

        [TestMethod]
        public void TrieContractItemSet1()
        {
            ITrie<String, char, String> trie = this.GetInstance();

            Assert.IsFalse(trie.ContainsKey("test"));

            trie["test"] = "a";

            Assert.IsTrue(trie.ContainsKey("test"));
            Assert.AreEqual("a", trie["test"]);
        }

        [TestMethod]
        public void TrieContractItemSet2()
        {
            ITrie<String, char, String> trie = this.GetInstance();

            Assert.IsFalse(trie.ContainsKey("test"));

            trie["test"] = "a";

            Assert.IsTrue(trie.ContainsKey("test"));
            Assert.AreEqual("a", trie["test"]);

            trie["test"] = "b";

            Assert.AreEqual("b", trie["test"]);
        }

        [TestMethod]
        public void TrieContractValues1()
        {
            ITrie<String, char, String> trie = this.GetInstance();

            Assert.IsFalse(trie.Values.Any());
        }

        [TestMethod]
        public void TrieContractValues2()
        {
            ITrie<String, char, String> trie = this.GetInstance();

            Assert.IsFalse(trie.Values.Any());

            trie.Add("test", "a");

            Assert.IsTrue(trie.Values.Any());
            Assert.AreEqual(1, trie.Values.Count());
        }

        [TestMethod]
        public void TrieContractValues3()
        {
            ITrie<String, char, String> trie = this.GetInstance();

            IEnumerable<String> values = trie.Values;

            Assert.IsFalse(values.Any());

            trie.Add("test", "a");

            Assert.IsTrue(values.Any());
            Assert.AreEqual(1, values.Count());
        }
    }

    [TestClass]
    public class TrieContractTests
        : AbstractTrieContractTests
    {
        protected override ITrie<string, char, String> GetInstance()
        {
            return new StringTrie<String>();
        }
    }

    [TestClass]
    public class TrieContractTests2
        : AbstractTrieContractTests
    {
        protected override ITrie<string, char, String> GetInstance()
        {
            return new Trie<String, char, String>(StringTrie<String>.KeyMapper);
        }
    }

    [TestClass]
    public class SparseTrieContractTests
        : AbstractTrieContractTests
    {
        protected override ITrie<string, char, string> GetInstance()
        {
            return new SparseStringTrie<String>();
        }
    }

    [TestClass]
    public class SparseTrieContractTests2
        : AbstractTrieContractTests
    {
        protected override ITrie<string, char, string> GetInstance()
        {
            return new SparseCharacterTrie<String, String>(StringTrie<String>.KeyMapper);
        }
    }

    [TestClass]
    public class SparseTrieContractTests3
        : AbstractTrieContractTests
    {
        protected override ITrie<string, char, string> GetInstance()
        {
            return new SparseValueTrie<String, char, String>(StringTrie<String>.KeyMapper);
        }
    }
}
