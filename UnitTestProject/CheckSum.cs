using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class CheckSum
    {
        List<byte> lst0 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        List<byte> lst1 = new List<byte> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        List<byte> lst2 = new List<byte> { 1, 1, 1, 1 };

        [TestMethod]
        public void FrameTestCheckSum1()
        {
            Frame nfra = new Frame.Builder().SetFrame(lst0).SetControlType(new CheckSumControl()).Create();
            Assert.AreEqual(nfra.GetControlPart().GetList().Sum(x => Convert.ToInt32(x)), 0);
        }
        [TestMethod]
        public void FrameTestCheckSum2()
        {
            Frame nfra = new Frame.Builder().SetFrame(lst2).SetControlType(new CheckSumControl()).Create();
            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(nfra.GetControlPart().GetList())), 4);
        }

        [TestMethod]
        public void FrameTestCheckSum3()
        {
            // 10(dec) = 1010 (bin)
            Frame nfra = new Frame.Builder().SetFrame(lst1).SetControlType(new CheckSumControl(),3).Create();
            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(nfra.GetControlPart().GetList())), 2);
        }

        [TestMethod]
        public void FrameTestCheckSum4()
        {
            // 10(dec) = 1010 (bin)
            Frame nfra = new Frame.Builder().SetFrame(lst1).SetControlType(new CheckSumControl(), 4).Create();
            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(nfra.GetControlPart().GetList())), 10);
        }
        [TestMethod]
        public void FrameTestCheckSum5()
        {
            // 10(dec) = 1010 (bin)
            Frame nfra = new Frame.Builder().SetFrame(lst1).SetControlType(new CheckSumControl(), 5).Create();
            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(nfra.GetControlPart().GetList())), 10);
        }

        [TestMethod]
        public void PackageTestCheckSum1()
        {
            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CheckSumControl(), 5).Create();
            Frame nfra2 = new Frame.Builder().SetFrame(lst0).SetControlType(new CheckSumControl(), 5).Create();
            Package pak = new Package();
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra2);
            pak.SetControlType(new CheckSumControl());
            pak.SetControlPartByType(4);

            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(pak.GetControlPart().GetList())), 10);
        }
        [TestMethod]
        public void PackageTestCheckSum2()
        {
            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CheckSumControl(), 4).Create();
            Package pak = new Package();
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra1);
            pak.SetControlType(new CheckSumControl());
            pak.SetControlPartByType(4);

            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(pak.GetControlPart().GetList())), 4);
        }

        [TestMethod]
        public void PackageTestCheckSum3()
        {
            // 10(dec) = 1010 (bin)
            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CheckSumControl(), 4).Create();
            Package pak = new Package();
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra1);
            pak.SetControlType(new CheckSumControl());
            pak.SetControlPartByType(1230);

            Assert.AreEqual(Convert.ToInt32(Helpers.GetPartInDec(pak.GetControlPart().GetList())), 20);
        }

        [TestMethod]
        public void PackageTestCheckSum4()
        {
            Package pak = new Package();
            pak.GenerateFrameList(100, 10, new CheckSumControl(), 2);
            Assert.AreEqual(pak.CheckPackage(), 3); // szansa na wystąpienie przypadku ze to niezadziała 0.0000000001%\
                                                    // wystąpienie bledu wynika z z tego ze na 1000 +czesc kontrolna bitach 
                                                    // musi wystapic 998 zer. Błąd odnosi sie jedynie do testu

        }
        [TestMethod]
        public void PackageTestCheckSum5()////////////////////
        {
            Package pak = new Package();
            pak.GenerateFrameList(100, 1000, new CheckSumControl(), 2);
            Console.WriteLine( pak[0].GetControlPart().GetControlPartInDec());
            //Assert.AreEqual(pak.CheckPackage(), 2); // szansa na wystąpienie przypadku ze to niezadziała 0.0000000001%\
                                                    // wystąpienie bledu wynika z z tego ze na 1000 +czesc kontrolna bitach 
                                                    // musi wystapic 998 zer. Błąd odnosi sie jedynie do testu
            Assert.AreEqual(pak[0].CheckFrame(), 3);

        }



    }
}
