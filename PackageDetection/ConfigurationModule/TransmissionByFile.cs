using Menu_GUI;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using PackageDetection.Menu_GUI;
using PackageDetection.MessageBuilderPackage;
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
        IMenuCollision menuCollision;
        TransmissionData transmissionData;
        int currentId;
        string fileName;

        System.Windows.Controls.Frame pSettings;
        System.Windows.Controls.Frame resultWindow;

        public TransmissionByFile(string fileName, ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            this.fileName = fileName;
            this.currentId = 0;
            this.pSettings = pSettings;
            this.resultWindow = resultWindow;
        }

        CollisionData GetCollisionType(XElement reader)
        {
            CollisionData cd = Helpers.CollisionDataFactory(((string)reader.Element("collisionType").Element("name")).ToLower());
            cd.SetComponentsByXML(reader);
            return cd;
        }

        public IMenuCollision GetMenuCollision()
        {
            return menuCollision;
        }

        public bool NextTransmission()
        {
            List<TransmissionData> transmissionLists =  (from e in XDocument.Load(fileName).Root.Elements("transmission")
                                where (int)e.Element("id") == this.currentId
                                select new TransmissionData
                                {
                                    interferenceLevel = (int)e.Element("interference_level"),
                                    sizeOfFrame = (int)e.Element("size_of_frame"),
                                    numbersOfFrameInPackage = (int)e.Element("numbers_of_frames_in_package"),
                                    numberOfTranssmision = (ulong)e.Element("number_of_transsmisions"),
                                    sizeControlPart = (int)e.Element("size_control_part"),
                                    controlType = (string)e.Element("control_type"),
                                    collisionType = this.GetCollisionType(e),
                                    numberOfPackagesToEnd = (ulong)e.Element("number_of_packages_to_end"),
                                    name = "_" + (int)e.Element("id") + "_" + (string)e.Element("transmission_name")

                                }).ToList();
            if (!(transmissionLists.Count < 1))
                transmissionData = transmissionLists[0];
            else
                return false;

            MessageBuilder.AddTitleMessage("++++" + transmissionData.name + "++++");

            menuCollision = Helpers.MenuCollisionFactory(transmissionData.collisionType.Name, ref resultWindow, ref pSettings);


            SetPackageSettings();
            currentId++;
            return true;
        }

        private void SetPackageSettings()
        {
            MessageBuilder.AddInfoMessage("Set size of control part : " + transmissionData.sizeControlPart);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsControlPart(transmissionData.sizeControlPart);

            MessageBuilder.AddInfoMessage("Set size of frame : " + transmissionData.sizeOfFrame);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsInFrame(transmissionData.sizeOfFrame);

            MessageBuilder.AddInfoMessage("Set number of frames in package : " + transmissionData.numbersOfFrameInPackage);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetFramesInPackage(transmissionData.numbersOfFrameInPackage);

            MessageBuilder.AddInfoMessage("Set interference level : " + transmissionData.interferenceLevel);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetInterferenceLVL(transmissionData.interferenceLevel);

            MessageBuilder.AddInfoMessage("Set namber of transmissions : " + transmissionData.numberOfTranssmision);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetNumberOfTransmission(transmissionData.numberOfTranssmision);

            MessageBuilder.AddInfoMessage("Set control type : " + transmissionData.controlType);
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetControlType(transmissionData.controlType);

            MessageBuilder.AddInfoMessage("Set number of packages to end : " + transmissionData.numberOfPackagesToEnd);
            menuCollision.GetMenuHandler().NumberOfPackagesToEnd = transmissionData.numberOfPackagesToEnd;

            //menuCollision.GetMenuHandler().GetResultsWindow.F

            
            menuCollision.SetComponentsByDictionary(transmissionData.collisionType.Args);
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
            menuCollision.StartTransmission(true);
        }

        public void SetComponentByName(string componentName, string value)
        {
            menuCollision.SetComponentByName(componentName, value);
        }

        public void SetComponentsByDictionary(Dictionary<string, int> d)
        {
            menuCollision.SetComponentsByDictionary(d);
        }

        public MenuHandler GetMenuHandler()
        {
            return menuCollision.GetMenuHandler();
        }
    }
}
//moja klasa, to jest dobra baza
//pieniadze sa prywatne, a stringi łososiokształtne