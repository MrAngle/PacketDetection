using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestGroupOfBitsCollision
    {
        [TestMethod]
        public void BitsCollisionTest1()
        {

            List<byte> nlst = new List<byte> { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 };
            Frame fra = new Frame.Builder().SetFrame(nlst).SetControl(5).Create();
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnFrame().ChangeGroupOfBits(0).Create();

            BC.DoCollision(fra, 10);
            List<byte> tnlst = new List<byte> { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
            Frame tfra = new Frame.Builder().SetFrame(tnlst).SetControl(5).Create();
            Assert.AreEqual(fra, tfra);

        }

        [TestMethod]
        public void BitsCollisionTest2()
        {
            List<byte> nlst = new List<byte> { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 };
            List<byte> ncon = new List<byte> { 1, 0, 1 };
            Frame fra = new Frame.Builder().SetFrame(nlst).SetControl(ncon).Create();
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnFrame().ChangeGroupOfBits(5).Create();

            BC.DoCollision(fra, 10);
            List<byte> tnlst = new List<byte> { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 };
            List<byte> tncon = new List<byte> { 0, 1, 0 };
            Frame tfra = new Frame.Builder().SetFrame(tnlst).SetControl(tncon).Create();
            Assert.AreEqual(fra, tfra);
        }
        [TestMethod]
        public void BitsCollisionTest3()
        {
            List<byte> nlst = new List<byte> { 1, 1, 1, 1, 0, 1, 1, 1, 1, 0 };
            List<byte> ncon = new List<byte> { 1, 0, 1 };
            Frame fra = new Frame.Builder().SetFrame(nlst).SetControl(ncon).Create();
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnFrame().ChangeGroupOfBits(0).Create();

            BC.DoCollision(fra, 20);
            List<byte> tnlst = new List<byte> { 0 ,0 ,0 ,0 , 1, 0, 0, 0, 0, 1 };
            List<byte> tncon = new List<byte> { 0, 1, 0 };
            Frame tfra = new Frame.Builder().SetFrame(tnlst).SetControl(tncon).Create();
            Assert.AreEqual(fra, tfra);
        }
    }
}
