
//using System.Collections.Generic;
//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Projekt_Kolko;

//namespace UnitTestProject
//{
//    [TestClass]
//    class PackageTest
//    {
//        List<byte> lst0 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
//        List<byte> lst1 = new List<byte> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
//        List<byte> lst2 = new List<byte> { 1, 1, 1, 1 };

//        [TestMethod]
//        public void PackIsChanged()
//        {
//            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new CRCControl(), 4).Create();
//            Package pak = new Package();
//            pak.AddFrame(nfra1);
//            pak.AddFrame(nfra1);
//            pak.SetControlType(new CRCControl());
//            pak.SetControlPartByType(4);
//            pak.ShowControlPart();

//            Assert.AreEqual(pak.GetControlPart().GetCount(), 3);
//        }
//    }
//}


