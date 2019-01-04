using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class ParityBitControlTest
    {

        List<byte> lst0 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        List<byte> lst1 = new List<byte> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        List<byte> lst2 = new List<byte> { 1, 1, 1, 1 };

        [TestMethod]
        public void ParityFrame1()
        {
            Frame nfra = new Frame.Builder().SetFrame(lst1).SetControlType(new ParityBitControl(), 8).Create();

            nfra.ShowInformationPart();
            nfra.ShowControlPart();
            //Assert.AreEqual(nfra.GetControlPart().GetList().Sum(x => Convert.ToInt32(x)), 0);
        }

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

        [TestMethod]
        public void CollisionDetectionParity()
        {
            ParityBitControl pB = new ParityBitControl();
            Frame nfra = new Frame.Builder().SetFrame(lst1).SetControlType(new ParityBitControl(), 8).Create();
            nfra[lst1.Count] = 1;
            nfra[lst1.Count-1] = 1;
            nfra[lst1.Count - 2] = 1;

            nfra.ShowInformationPart();
            nfra.ShowControlPart();

            

            Console.WriteLine("\n wynik: " + nfra.CheckFrame() ); 

            //Assert.AreEqual(nfra.GetControlPart().GetList().Sum(x => Convert.ToInt32(x)), 0);
        }
    }
}
