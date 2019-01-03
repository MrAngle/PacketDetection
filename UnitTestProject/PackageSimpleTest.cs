using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTestProject
{
    [TestClass]
    public class PackageSimpleTest
    {

        List<byte> lst0 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        List<byte> lst1 = new List<byte> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        List<byte> lst2 = new List<byte> { 1, 1, 1, 1 };

        [TestMethod]
        public void PackIsChanged()
        {
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(true, 1).ChangeGroupOfBits(lst0.Count).Create();

            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new ParityBitControl(), 1).Create();
            Frame nfra2 = new Frame.Builder().SetFrame(lst0).SetControlType(new ParityBitControl(), 1).Create();

            Package pak = new Package();

            pak.AddFrame(nfra1);
            pak.AddFrame(nfra2);
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra2);

            pak.SetControlType(new ParityBitControl());
            pak.SetControlPartByType(1);

            pak.ShowFrames();
            pak.ShowControlPart();


            BC.DoCollision(pak, 2);

            pak.ShowFrames();
            pak.ShowControlPart();

            Assert.AreEqual(pak.IsChanged(), true);
        }

        [TestMethod]
        public void PackIsChanged2()
        {
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(true, 1).ChangeGroupOfBits(lst0.Count).Create();

            Frame nfra1 = new Frame.Builder().SetFrame(lst1).SetControlType(new ParityBitControl(), 1).Create();
            Frame nfra2 = new Frame.Builder().SetFrame(lst0).SetControlType(new ParityBitControl(), 1).Create();

            Package pak = new Package();

            pak.AddFrame(nfra1);
            pak.AddFrame(nfra2);
            pak.AddFrame(nfra1);
            pak.AddFrame(nfra2);

            pak.SetControlType(new ParityBitControl());
            pak.SetControlPartByType(1);

            pak.ShowFrames();
            pak.ShowControlPart();


            BC.DoCollision(pak, 2);

            pak.ShowFrames();
            pak.ShowControlPart();

            Assert.AreEqual(pak.IsChanged(), true);
        }
    }
}
