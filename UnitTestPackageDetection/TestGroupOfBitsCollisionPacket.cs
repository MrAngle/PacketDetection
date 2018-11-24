using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class TestGroupOfBitsCollisionPacket
    {
        [TestMethod]
        public void TestGroupOfBitsCollisionPacket1()
        {
            Package pak = new Package();
            List<byte> nlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> ncon1 = new List<byte> { 1, 1, 1 };
            Frame fra1 = new Frame.Builder().SetFrame(nlst1).SetControl(ncon1).Create();

            List<byte> nlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> ncon2 = new List<byte> { 0, 0, 0 };
            Frame fra2 = new Frame.Builder().SetFrame(nlst2).SetControl(ncon2).Create();

            List<byte> pcon2 = new List<byte> { 0, 0, 0 };

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.GetControlPart().SetControlPart(pcon2);

            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(0).ChangeGroupOfBits(0).Create();
            BC.DoCollision(pak, 12);

            Package tpak = new Package();
            List<byte> tnlst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tncon1 = new List<byte> { 0, 0, 1 };
            Frame tfra1 = new Frame.Builder().SetFrame(tnlst1).SetControl(tncon1).Create();

            List<byte> tnlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tncon2 = new List<byte> { 0, 0, 0 };
            Frame tfra2 = new Frame.Builder().SetFrame(tnlst2).SetControl(tncon2).Create();

            List<byte> tpcon2 = new List<byte> { 0, 0, 0 };

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.GetControlPart().SetControlPart(tpcon2);

            Assert.AreEqual(pak, tpak);
            
        }

        [TestMethod]
        public void TestGroupOfBitsCollisionPacket2()
        {
            Package pak = new Package();
            List<byte> nlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> ncon1 = new List<byte> { 1, 1, 1 };
            Frame fra1 = new Frame.Builder().SetFrame(nlst1).SetControl(ncon1).Create();

            List<byte> nlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> ncon2 = new List<byte> { 0, 0, 0 };
            Frame fra2 = new Frame.Builder().SetFrame(nlst2).SetControl(ncon2).Create();

            List<byte> pcon2 = new List<byte> { 0, 0, 0 };

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.GetControlPart().SetControlPart(pcon2);

            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(1).ChangeGroupOfBits(0).Create();
            BC.DoCollision(pak, 10);

            Package tpak = new Package();
            List<byte> tnlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> tncon1 = new List<byte> { 1, 1, 1 };
            Frame tfra1 = new Frame.Builder().SetFrame(tnlst1).SetControl(tncon1).Create();

            List<byte> tnlst2 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> tncon2 = new List<byte> { 0, 0, 0 };
            Frame tfra2 = new Frame.Builder().SetFrame(tnlst2).SetControl(tncon2).Create();

            List<byte> tpcon2 = new List<byte> { 0, 0, 0 };

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.GetControlPart().SetControlPart(tpcon2);

            Assert.AreEqual(pak, tpak);

        }

        [TestMethod]
        public void TestGroupOfBitsCollisionPacket3()
        {
            Package pak = new Package();
            List<byte> nlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> ncon1 = new List<byte> { 1, 1, 1 };
            Frame fra1 = new Frame.Builder().SetFrame(nlst1).SetControl(ncon1).Create();

            List<byte> nlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> ncon2 = new List<byte> { 0, 0, 0 };
            Frame fra2 = new Frame.Builder().SetFrame(nlst2).SetControl(ncon2).Create();

            List<byte> pcon2 = new List<byte> { 0, 0, 0 };

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.GetControlPart().SetControlPart(pcon2);

            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(1).ChangeGroupOfBits(0).Create();
            BC.DoCollision(pak, 26);

            Package tpak = new Package();
            List<byte> tnlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> tncon1 = new List<byte> { 1, 1, 1 };
            Frame tfra1 = new Frame.Builder().SetFrame(tnlst1).SetControl(tncon1).Create();

            List<byte> tnlst2 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> tncon2 = new List<byte> { 1, 1, 1 };
            Frame tfra2 = new Frame.Builder().SetFrame(tnlst2).SetControl(tncon2).Create();

            List<byte> tpcon2 = new List<byte> { 1, 1, 1 };

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.GetControlPart().SetControlPart(tpcon2);

            Assert.AreEqual(pak, tpak);

        }

        [TestMethod]
        public void TestGroupOfBitsCollisionPacket4()
        {
            Package pak = new Package();
            List<byte> nlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> ncon1 = new List<byte> { 1, 1, 1 };
            Frame fra1 = new Frame.Builder().SetFrame(nlst1).SetControl(ncon1).Create();

            List<byte> nlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> ncon2 = new List<byte> { 0, 0, 0 };
            Frame fra2 = new Frame.Builder().SetFrame(nlst2).SetControl(ncon2).Create();

            List<byte> pcon2 = new List<byte> { 0, 0, 0 };

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.GetControlPart().SetControlPart(pcon2);

            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(0).ChangeGroupOfBits(5).Create();
            BC.DoCollision(pak, 15);

            Package tpak = new Package();
            List<byte> tnlst1 = new List<byte> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            List<byte> tncon1 = new List<byte> { 0, 0, 0 };
            Frame tfra1 = new Frame.Builder().SetFrame(tnlst1).SetControl(tncon1).Create();

            List<byte> tnlst2 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 };
            List<byte> tncon2 = new List<byte> { 0, 0, 0 };
            Frame tfra2 = new Frame.Builder().SetFrame(tnlst2).SetControl(tncon2).Create();

            List<byte> tpcon2 = new List<byte> { 0, 0, 0 };

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.GetControlPart().SetControlPart(tpcon2);

            Assert.AreEqual(pak, tpak);

        }

        [TestMethod]
        public void TestGroupOfBitsCollisionPacket5()
        {
            Package pak = new Package();
            List<byte> nlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            List<byte> ncon1 = new List<byte> { 1, 1, 1 };
            Frame fra1 = new Frame.Builder().SetFrame(nlst1).SetControl(ncon1).Create();

            List<byte> nlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> ncon2 = new List<byte> { 0, 0, 0 };
            Frame fra2 = new Frame.Builder().SetFrame(nlst2).SetControl(ncon2).Create();

            List<byte> pcon2 = new List<byte> { 0, 0, 0 };

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.GetControlPart().SetControlPart(pcon2);

            BitsCollision BC = new BitsCollision.Builder().SetBasedOnFrame().ChangeGroupOfBits(9).Create();
            BC.DoCollision(pak, 3);

            Package tpak = new Package();
            List<byte> tnlst1 = new List<byte> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
            List<byte> tncon1 = new List<byte> { 0, 0, 1 };
            Frame tfra1 = new Frame.Builder().SetFrame(tnlst1).SetControl(tncon1).Create();

            List<byte> tnlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            List<byte> tncon2 = new List<byte> { 1, 1, 0 };
            Frame tfra2 = new Frame.Builder().SetFrame(tnlst2).SetControl(tncon2).Create();

            List<byte> tpcon2 = new List<byte> { 0, 0, 0 };

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.GetControlPart().SetControlPart(tpcon2);

            Assert.AreEqual(pak, tpak);

        }
    }
}
