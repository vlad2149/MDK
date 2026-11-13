using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSysLib;

namespace NumSysLib.Tests
{
    [TestClass]
    public class AdapterTests
    {
        private Adapter adapter = null!;

        [TestInitialize]
        public void CreateAdapter()
        {
            adapter = new Adapter();
        }

        [TestMethod]
        public void BinaryToDecimal()
        {
            Assert.AreEqual("42", adapter.Convert("101010", 2, 10));
        }

        [TestMethod]
        public void DecimalToBinary()
        {
            Assert.AreEqual("101010", adapter.Convert("42", 10, 2));
        }

        [TestMethod]
        public void OctToDecimal()
        {
            Assert.AreEqual("63", adapter.Convert("77", 8, 10));
        }

        [TestMethod]
        public void DecimalToOct()
        {
            Assert.AreEqual("77", adapter.Convert("63", 10, 8));
        }

        [TestMethod]
        public void HexToDecimal()
        {
            Assert.AreEqual("255", adapter.Convert("FF", 16, 10));
        }

        [TestMethod]
        public void DecimalToHex()
        {
            Assert.AreEqual("FF", adapter.Convert("255", 10, 16));
        }

        [TestMethod]
        public void BinaryToHexDirectly()
        {
            Assert.AreEqual("FF", adapter.Convert("11111111", 2, 16));
        }

        [TestMethod]
        public void AdapterCanConvertSeveralValues()
        {
            Assert.AreEqual("255", adapter.Convert("FF", 16, 10));
            Assert.AreEqual("10", adapter.Convert("1010", 2, 10));
        }

        [TestMethod]
        public void InvalidDigitThrowsException()
        {
            Assert.Throws<ArgumentException>(() => adapter.Convert("102", 2, 10));
        }

        [TestMethod]
        public void InvalidBaseThrowsException()
        {
            Assert.Throws<ArgumentException>(() => adapter.Convert("10", 3, 2));
        }
    }
}
