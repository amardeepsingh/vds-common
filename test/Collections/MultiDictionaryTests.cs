﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VDS.Common.Collections
{
    [TestClass]
    public class MultiDictionaryTests
    {
        [TestMethod]
        public void MultiDictionary1()
        {
            MultiDictionary<Double, Int32> dict = new MultiDictionary<Double, Int32>();
        }
    }
}
