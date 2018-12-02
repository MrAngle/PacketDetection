using Menu_GUI;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using PackageDetection.Menu_GUI;
using PackageDetection.MessageBuilderPackage;
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule
{
    public class TransmissionByFile
    {
        //MenuHandler menuHandler;
        TransmissionData transmissionData;
        int currentId;
        string fileName;

        //System.Windows.Controls.Frame pSettings;
        //System.Windows.Controls.Frame resultWindow;

        public TransmissionByFile(string fileName)
        {
            this.fileName = fileName;
            this.currentId = 0;

        }

        CollisionData GetCollisionType(XElement reader)
        {
            CollisionData cd = Helpers.CollisionDataFactory(((string)reader.Element("collisionType").Element("name")).ToLower());
            cd.SetComponentsByXML(reader);
            return cd;
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
                                    name = "_" + (int)e.Element("id") + "_" + (string)e.Element("transmission_name") + "_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss", CultureInfo.InvariantCulture)

            }).ToList();
            if (!(transmissionLists.Count < 1))
                transmissionData = transmissionLists[0];
            else
                return false;

            MessageBuilder.AddTitleMessage("++++" + transmissionData.name + "++++");

            MenuHandler.MenuCollision = Helpers.MenuCollisionFactory(transmissionData.collisionType.Name);


            SetPackageSettings();
            currentId++;
            return true;
        }

        private void SetPackageSettings()
        {
            MessageBuilder.AddInfoMessage("Set size of control part : " + transmissionData.sizeControlPart);
            MenuHandler.GetMenuPackageSettings().SetBitsControlPart(transmissionData.sizeControlPart);

            MessageBuilder.AddInfoMessage("Set size of frame : " + transmissionData.sizeOfFrame);
            MenuHandler.GetMenuPackageSettings().SetBitsInFrame(transmissionData.sizeOfFrame);

            MessageBuilder.AddInfoMessage("Set number of frames in package : " + transmissionData.numbersOfFrameInPackage);
            MenuHandler.GetMenuPackageSettings().SetFramesInPackage(transmissionData.numbersOfFrameInPackage);

            MessageBuilder.AddInfoMessage("Set interference level : " + transmissionData.interferenceLevel);
            MenuHandler.GetMenuPackageSettings().SetInterferenceLVL(transmissionData.interferenceLevel);

            MessageBuilder.AddInfoMessage("Set number of transmissions : " + transmissionData.numberOfTranssmision);
            MenuHandler.GetMenuPackageSettings().SetNumberOfTransmission(transmissionData.numberOfTranssmision);

            MessageBuilder.AddInfoMessage("Set control type : " + transmissionData.controlType);
            MenuHandler.GetMenuPackageSettings().SetControlType(transmissionData.controlType);

            MessageBuilder.AddInfoMessage("Set number of packages to end : " + transmissionData.numberOfPackagesToEnd);
            MenuHandler.NumberOfPackagesToEnd = transmissionData.numberOfPackagesToEnd;

            //menuCollision.GetMenuHandler().GetResultsWindow.F


            MenuHandler.MenuCollision.SetComponentsByDictionary(transmissionData.collisionType.Args);
        }

        public void ConfigSetComponentByName(string componentName, string value)
        {
            MenuHandler.MenuCollision.SetComponentByName(componentName, value);
        }


        public void StartTransmission()
        {
            try
            {
                MenuHandler.Collision = MenuHandler.MenuCollision.CreateCollision();
                MenuHandler.StartTransmission(transmissionData.name);
                //menuCollision.GetMenuHandler().StartTranssmision(setConfigurationByFile);
                //menuHandler.GetMenuPackageSettings().Start_transsmision(BC, menuHandler.GetResultsWindow(), menuHandler.NumberOfPackagesToEnd);
            }
            catch (FormatException)
            {
                //BC = null;
                //MessageBox.Show("Wprowadz dane");
            }
        }

        public void SetComponentByName(string componentName, string value)
        {
            MenuHandler.MenuCollision.SetComponentByName(componentName, value);
        }

        public void SetComponentsByDictionary(Dictionary<string, int> d)
        {
            MenuHandler.MenuCollision.SetComponentsByDictionary(d);
        }

    }
}
//moja klasa, to jest dobra baza
//pieniadze sa prywatne, a stringi łososiokształtne