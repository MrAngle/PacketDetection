using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class RandomCollision
    {
        private int Check(ref Package pak,ref Package tpak)
        {
            int count = 0;
            for (int i = 0; i < tpak.GetFrames().Count; i++)
            {
                for (int j = 0; j < tpak.GetFrameCount(); j++)
                {
                    if (tpak[i][j] != pak[i][j])
                        count++;
                }
            }
            for (int i = 0; i < tpak.GetControlPart().GetCount(); i++)
            {
                if (tpak.GetControlPart()[i] != pak.GetControlPart()[i])
                    count++;
            }
            return count;
        }

        [TestMethod]
        public void RandomCollision1()
        {
            //SPRAWDZ
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(true, 0).Create();
            Package pak = new Package();

            List<byte> lst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con1 = new List<byte> { 0, 0, 0, 0};

            List<byte> lst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con2 = new List<byte> { 0, 0, 0, 0 };

            List<byte> lst3 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con3 = new List<byte> { 0, 0, 0, 0 };
            Frame fra1 = new Frame.Builder().SetFrame(lst1).SetControl(con1).Create();
            Frame fra2 = new Frame.Builder().SetFrame(lst2).SetControl(con2).Create();
            Frame fra3 = new Frame.Builder().SetFrame(lst3).SetControl(con3).Create();

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.AddFrame(fra3);
            pak.GetControlPart().SetControlPart(10);

            Package tpak = new Package();

            List<byte> tlst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon1 = new List<byte> { 0, 0, 0, 0 };

            List<byte> tlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon2 = new List<byte> { 0, 0, 0, 0 };

            List<byte> tlst3 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon3 = new List<byte> { 0, 0, 0, 0 };
            Frame tfra1 = new Frame.Builder().SetFrame(tlst1).SetControl(tcon1).Create();
            Frame tfra2 = new Frame.Builder().SetFrame(tlst2).SetControl(tcon2).Create();
            Frame tfra3 = new Frame.Builder().SetFrame(tlst3).SetControl(tcon3).Create();

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.AddFrame(tfra3);
            tpak.GetControlPart().SetControlPart(10);


            BC.DoCollision(pak, 10);


            

            Assert.AreEqual(Check(ref pak, ref tpak), 10);


        }
        [TestMethod]
        public void RandomCollision2()
        {
            //BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage().SetRandomCollision().Create();
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(true, 0).Create();
            Package pak = new Package();

            List<byte> lst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con1 = new List<byte> { 0, 0, 0, 0 };

            List<byte> lst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con2 = new List<byte> { 0, 0, 0, 0 };

            List<byte> lst3 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con3 = new List<byte> { 0, 0, 0, 0 };
            Frame fra1 = new Frame.Builder().SetFrame(lst1).SetControl(con1).Create();
            Frame fra2 = new Frame.Builder().SetFrame(lst2).SetControl(con2).Create();
            Frame fra3 = new Frame.Builder().SetFrame(lst3).SetControl(con3).Create();

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.AddFrame(fra3);
            pak.GetControlPart().SetControlPart(10);

            Package tpak = new Package();

            List<byte> tlst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon1 = new List<byte> { 0, 0, 0, 0 };

            List<byte> tlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon2 = new List<byte> { 0, 0, 0, 0 };

            List<byte> tlst3 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon3 = new List<byte> { 0, 0, 0, 0 };
            Frame tfra1 = new Frame.Builder().SetFrame(tlst1).SetControl(tcon1).Create();
            Frame tfra2 = new Frame.Builder().SetFrame(tlst2).SetControl(tcon2).Create();
            Frame tfra3 = new Frame.Builder().SetFrame(tlst3).SetControl(tcon3).Create();

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.AddFrame(tfra3);
            tpak.GetControlPart().SetControlPart(10);


            BC.DoCollision(pak, 500);



            Assert.AreEqual(Check(ref pak, ref tpak), 76);


        }

        [TestMethod]
        public void RandomCollision3()
        {
            //BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage().SetRandomCollision().Create();
            BitsCollision BC = new BitsCollision.Builder().SetBasedOnPackage(true, 0).Create();
            Package pak = new Package();

            List<byte> lst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con1 = new List<byte> { 0, 0, 0, 0 };

            List<byte> lst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con2 = new List<byte> { 0, 0, 0, 0 };

            List<byte> lst3 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> con3 = new List<byte> { 0, 0, 0, 0 };
            Frame fra1 = new Frame.Builder().SetFrame(lst1).SetControl(con1).Create();
            Frame fra2 = new Frame.Builder().SetFrame(lst2).SetControl(con2).Create();
            Frame fra3 = new Frame.Builder().SetFrame(lst3).SetControl(con3).Create();

            pak.AddFrame(fra1);
            pak.AddFrame(fra2);
            pak.AddFrame(fra3);
            pak.GetControlPart().SetControlPart(10);

            Package tpak = new Package();

            List<byte> tlst1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon1 = new List<byte> { 0, 0, 0, 0 };

            List<byte> tlst2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon2 = new List<byte> { 0, 0, 0, 0 };

            List<byte> tlst3 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<byte> tcon3 = new List<byte> { 0, 0, 0, 0 };
            Frame tfra1 = new Frame.Builder().SetFrame(tlst1).SetControl(tcon1).Create();
            Frame tfra2 = new Frame.Builder().SetFrame(tlst2).SetControl(tcon2).Create();
            Frame tfra3 = new Frame.Builder().SetFrame(tlst3).SetControl(tcon3).Create();

            tpak.AddFrame(tfra1);
            tpak.AddFrame(tfra2);
            tpak.AddFrame(tfra3);
            tpak.GetControlPart().SetControlPart(10);


            BC.DoCollision(pak, 0);



            Assert.AreEqual(Check(ref pak, ref tpak), 0);


        }
    }
}
