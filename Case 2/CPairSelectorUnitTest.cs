using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChoosePairs;

namespace ChoosePairsUnitTest
{
    [TestClass]
    public class CPairSelectorUnitTest
    {
        [TestMethod]
        public void Select_CheckValues()
        {
            const Int32 X = 7;
            ICollection<Int32> testArray = new[] { 1, -3, 9, 10, 11, 12 };
            var pairs = CPairSelector.Select(testArray, X);
            
            Assert.AreEqual<Int32>(pairs.Count(), 1);
            
            foreach (var pair in pairs)
            {
                Assert.IsTrue((pair.Item1 == -3 && pair.Item2 == 10) || (pair.Item1 == 10 && pair.Item2 == -3));
            }
        }

        [TestMethod]
        public void Select_CheckCount()
        {
            const Int32 X = 98;
            ICollection<Int32> testArrayY = new[] { -23, 17, 121, 6, 9, 95, 96, 7, 2, 5, 3, 108, -10, 578, 56, -480 };
            var pairs = CPairSelector.Select(testArrayY, X);

            Assert.AreEqual<Int32>(pairs.Count(), 5);
        }

        [TestMethod]        
        [ExpectedException(typeof(ArgumentNullException))]
        public void Select_CollectionIsNullException()
        {
            CPairSelector.Select(null, 0).Any();
        }
    }
}
