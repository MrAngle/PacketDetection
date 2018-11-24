using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Kolko;

namespace UnitTests
{
    [TestClass]
    public class InfiniteNumberTest
    {
        [TestMethod]
        public void InfiniteNumberTest1()
        {
            //Ulong Maxvalue = 18446744073709551615
            InfiniteNumber n1 = new InfiniteNumber(1234567890);
            Assert.AreEqual(n1.ToString(), "1234567890");
        }
        [TestMethod]
        public void InfiniteNumberTest2()
        {
            //Ulong Maxvalue = 18446744073709551615
            //                  1000000000000000000
            //                   999999999999999999
            
            InfiniteNumber n1 = new InfiniteNumber();
            n1.O_Add(999999999999999999);
            Assert.AreEqual(n1.ToString(), "999999999999999999");
        }
        [TestMethod]
        public void InfiniteNumberTest3()
        {
            //Ulong Maxvalue = 18446744073709551615
            //                  1000000000000000000
            //                   999999999999999999

            InfiniteNumber n1 = new InfiniteNumber();
            n1.O_Add(999999999999999999);
            n1.O_Add(2);
            Assert.AreEqual(n1.ToString(), "1000000000000000001");
        }

        [TestMethod]
        public void InfiniteNumberTest4()
        {
            //Ulong Maxvalue = 18446744073709551615
            //                  1000000000000000000
            //                   999999999999999999

            InfiniteNumber n1 = new InfiniteNumber();
            n1.O_Add(999999999999999999);
            n1.O_Add(1);
            Console.WriteLine(n1);
            Assert.AreEqual(n1.ToString(), "1000000000000000000");
        }

        [TestMethod]
        public void InfiniteNumberTest5()
        {
            //Ulong Maxvalue = 18446744073709551615
            //                  1000000000000000000
            //                   999999999999999999

            InfiniteNumber n1 = new InfiniteNumber();
            n1.O_Add(999999999999999999);
            //       1000000000000000000
            n1.O_Add(999999999999999999);
            n1.O_Add(3);
            Console.WriteLine(n1);
            Assert.AreEqual(n1.ToString(), "2000000000000000001");
        }

        [TestMethod]
        public void InfiniteNumberTest6()
        {
            InfiniteNumber n1 = new InfiniteNumber();
            n1.O_Add(9);

            Assert.AreEqual(n1.ToString(), "9");
        }
    }
}
