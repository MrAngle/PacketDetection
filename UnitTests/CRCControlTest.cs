using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class CRCControlTest
    {
        List<byte> lst0 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        List<byte> lst1 = new List<byte> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        List<byte> lst2 = new List<byte> { 1, 1, 1, 1 };
        [TestMethod]
        public void SetCRCPackage1()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl(), 4).Create();
            Package pak = new Package();
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra1);
            pak.SetControlType(new CRCControl());
            pak.SetControlPartByType(4);
            pak.ShowControlPart();

            Assert.AreEqual(pak.GetControlPart().GetCount(), 4);

        }

        [TestMethod]
        public void SetCRCPackage2()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl()).Create();
            Package pak = new Package();
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra1);
            pak.SetControlType(new CRCControl());
            pak.SetControlPartByType();
            pak.ShowControlPart();

            Assert.AreEqual(pak.GetControlPart().GetCount(), 5);

        }

        [TestMethod]
        public void SetFrameCRC1()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl(), 4).Create();
            nfra1.ShowControlPart();

            Assert.AreEqual(nfra1.GetControlPart().GetCount(), 4);

        }

        [TestMethod]
        public void SetFrameCRC2()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl()).Create();
            //nfra1.ShowControlPart();

            Assert.AreEqual(nfra1.GetControlPart().GetCount(), 4);

        }

        [TestMethod]
        public void CollisionFrameCRC1()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl(), 4).Create();
            //nfra1.ShowControlPart();
            

            Assert.AreEqual(nfra1.CheckFrame(), 0);

        }
        [TestMethod]
        public void CollisionFrameCRC2()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl()).Create();
            //nfra1.ShowControlPart();


            Assert.AreEqual(nfra1.CheckFrame(), 0);

        }
        [TestMethod]
        public void CollisionFrameCRC3()
        {

            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl(), 123298).Create();
            //nfra1.ShowControlPart();


            Assert.AreEqual(nfra1.CheckFrame(), 0);

        }

        [TestMethod]
        public void CollisionPackageCRC1()
        {
            Package pak = new Package();
            pak.GenerateFrameList(10, 5, new CRCControl(), 5);
            Assert.AreEqual(pak.CheckPackage(), 0);

        }

        [TestMethod]
        public void CollisionPackageCRC2()
        {
            Package pak = new Package();
            pak.GenerateFrameList(10, 5, new CRCControl());
            Assert.AreEqual(pak.CheckPackage(), 0);

        }

        [TestMethod]
        public void CollisionPackageCRC3()
        {
            Package pak = new Package();
            pak.GenerateFrameList(10, 5, new CRCControl(), 2);
            Assert.AreEqual(pak.CheckPackage(), 0);

        }

        [TestMethod]
        public void CollisionPackageCRC4()
        {
            Package pak = new Package();
            pak.GenerateFrameList(100, 5, new CRCControl(), 1234);
            Assert.AreEqual(pak.CheckPackage(), 0);

        }

        [TestMethod]
        public void CollisionPackageCRC5()///////////////////////////////
        {
            Package pak = new Package();
            pak.GenerateFrameList(100, 1000, new CRCControl(), 1);
            //Assert.AreEqual(pak.CheckPackage(), 2); // szansa na wystąpienie przypadku ze to niezadziała 0.0000000001%\
            // wystąpienie bledu wynika z z tego ze na 1000 +czesc kontrolna bitach 
            // musi wystapic 998 zer.
            Assert.AreEqual(pak.CheckPackage(), 0);

        }

    }
}
