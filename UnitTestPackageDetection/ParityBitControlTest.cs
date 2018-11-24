using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class ParityBitControlTest
    {
        [TestMethod]
        public void ParityPackage1()
        {
            Package pak = new Package();
            pak.GenerateFrameList(10, 10, new ParityBitControl(), 5);
            Assert.AreEqual(pak.CheckPackage(), 0);
        }
        [TestMethod]
        public void ParityPackage2()
        {
            Package pak = new Package();
            pak.GenerateFrameList(10, 10, new ParityBitControl());
            Assert.AreEqual(pak.CheckPackage(), 0);
        }

        [TestMethod]
        public void ParityPackage3()
        {
            Package pak = new Package();
            pak.GenerateFrameList(100, 10, new ParityBitControl(),0);
            Assert.AreEqual(pak.CheckPackage(), 0);
        }
    }
}
