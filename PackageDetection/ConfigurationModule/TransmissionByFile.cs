using Menu_GUI;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule
{
    public class TransmissionByFile
    {
        MenuCollision menuCollision;
        TransmissionData transmissionList;
        int currentId;
        string fileName;

        public TransmissionByFile(string fileName)
        {
            this.fileName = fileName;
            this.currentId = 0;
            // Loading from a file, you can also load from a stream
            //var xml = XDocument.Load(@"C:\Users\lipin\source\repos\PackageDetection\PackageDetection\XMLFiles\contacts.xml");

            ////// Query the data and write out a subset of contacts
            ////var query = from c in xml.Root.Descendants("contact")
            ////            where (int)c.Attribute("id") < 4
            ////            select c.Element("firstName").Value + " " +
            ////                   c.Element("lastName").Value;

            //Console.WriteLine("szukam");
            //foreach (string name in query)
            //{
            //    Console.WriteLine("znalazlem");
            //    Console.WriteLine("Contact's Full Name: {0}", name);
            //}
            //    public int interferenceLevel;
            //public int sizeOfFrame;
            //public int numbersOfFrameInPackage;
            //public ulong numberOfTranssmision;
            //public int sizeControlPart;

           
            //StringBuilder result = new StringBuilder();

            ////Load xml
            //XDocument xdoc = XDocument.Load(@"C:\Users\lipin\source\repos\PackageDetection\PackageDetection\XMLFiles\contacts.xml");

            ////Run query
            //var lv1s = from lv1 in xdoc.Descendants("level1")
            //           where (int)lv1.Attribute("name") < 4
            //           select new
            //           {
            //               Header = lv1.Attribute("name").Value,
            //               Children = lv1.Descendants("level2")
            //           };

            ////Loop through results
            //foreach (var lv1 in lv1s)
            //{
            //    result.AppendLine(lv1.Header);
            //    foreach (var lv2 in lv1.Children)
            //        result.AppendLine("     " + lv2.Attribute("name").Value);
            //}

            //Console.WriteLine(result);
        }

        CollisionData getCollisionType(XElement reader)
        {
            CollisionData cd = Helpers.CollisionDataFactory((string)reader.Element("collisionType").Element("name"));
            cd.SetComponentsByXML(reader);
            return cd;
        }

        public MenuCollision GetMenuCollision()
        {
            return menuCollision;
        }

        public int NextTransmission(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            transmissionList = (from e in XDocument.Load(fileName).Root.Elements("transmission")
                                where (int)e.Element("id") == this.currentId
                                select new TransmissionData
                                {
                                    interferenceLevel = (int)e.Element("interference_level"),
                                    sizeOfFrame = (int)e.Element("size_of_frame"),
                                    numbersOfFrameInPackage = (int)e.Element("numbers_of_frames_in_package"),
                                    numberOfTranssmision = (ulong)e.Element("number_of_transsmisions"),
                                    sizeControlPart = (int)e.Element("size_control_part"),
                                    collisionType = this.getCollisionType(e)

                                }).ToList()[0];

            //foreach (var name in transmissionList)
            //{
            //    Console.WriteLine(name.interferenceLevel);
            //    Console.WriteLine(name.sizeOfFrame);
            //    Console.WriteLine(name.numbersOfFrameInPackage);
            //    Console.WriteLine(name.numberOfTranssmision);
            //    Console.WriteLine(name.sizeControlPart);
            //}

            menuCollision = Helpers.MenuCollisionFactory(Helpers.BIT_COLLISION, ref resultWindow, ref pSettings);
            menuCollision.SetComponentsByDictionary(transmissionList.collisionType.Args);
            SetPackageSettings();
            return 1;
        }

        private void SetPackageSettings()
        {
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsControlPart(transmissionList.sizeControlPart);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsInFrame(43);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetFramesInPackage(342);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetInterferenceLVL(234);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetNumberOfTransmission(23);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetControlType(true);
        }

        public void ConfigSetComponentByName(string componentName, string value)
        {
            menuCollision.SetComponentByName(componentName, value);
        }

        public void SClose()
        {
            menuCollision.SClose();
        }

        public void StartTransmission()
        {
            menuCollision.StartTransmission();
        }

    }
}
//moja klasa, to jest dobra baza
//pieniadze sa prywatne, a stringi łososiokształtne