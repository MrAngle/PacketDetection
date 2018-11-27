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
        }

        CollisionData getCollisionType(XElement reader)
        {
            CollisionData cd = Helpers.CollisionDataFactory(((string)reader.Element("collisionType").Element("name")).ToLower());
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
                                    controlType = (string)e.Element("control_type"),
                                    collisionType = this.getCollisionType(e)

                                }).ToList()[0];

            menuCollision = Helpers.MenuCollisionFactory(transmissionList.collisionType.Name, ref resultWindow, ref pSettings);

                Console.WriteLine(transmissionList.interferenceLevel);
                Console.WriteLine(transmissionList.sizeOfFrame);
                Console.WriteLine(transmissionList.numbersOfFrameInPackage);
                Console.WriteLine(transmissionList.numberOfTranssmision);
                Console.WriteLine(transmissionList.sizeControlPart);



            SetPackageSettings();
            return 1;
        }

        private void SetPackageSettings()
        {
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsControlPart(transmissionList.sizeControlPart);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsInFrame(transmissionList.sizeOfFrame);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetFramesInPackage(transmissionList.numbersOfFrameInPackage);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetInterferenceLVL(transmissionList.interferenceLevel);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetNumberOfTransmission(transmissionList.numberOfTranssmision);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetControlType(transmissionList.controlType);
            menuCollision.SetComponentsByDictionary(transmissionList.collisionType.Args);
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