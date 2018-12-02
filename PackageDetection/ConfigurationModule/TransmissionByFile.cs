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

        //private int checkInterferenceLevelElement(XElement reader)
        //{
        //    int returnValue = 50;
        //    //XElement value = reader.Element("interference_level");
        //    if (reader == null)
        //    {
        //        MessageBuilder.AddWarnMessage("interference_level has not been set. The default value has been set(50)");
        //    }
        //    else
        //    {
        //        if ((int)reader < 10000 || (int)reader >= 0)
        //            returnValue = (int)reader;
        //        else
        //            MessageBuilder.AddWarnMessage("interference_level - the set number is not in the range (0-10000). The default value has been set(50)");
        //    }
        //    return returnValue;
        //}

        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }


        private T CheckNumberElement<T>(XElement reader, T maxNumber, T minNumber, T defaultValue, string name)
        {
            T returnValue = (T)Convert.ChangeType(defaultValue, typeof(T));
            //XElement value = reader.Element("interference_level");
            if (reader == null)
            {
                MessageBuilder.AddWarnMessage(name + " has not been set. The default value has been set("+ defaultValue +")");
            }
            else
            {
                dynamic tempValue;
                try
                {
                    tempValue = ConvertValue<T, string>(reader.Value);
                    if (tempValue > minNumber && tempValue < maxNumber)
                        returnValue = (T)tempValue;
                    else
                        MessageBuilder.AddWarnMessage(reader.Name + " - the set number is not in the range (" + minNumber + "- " + maxNumber + "). The default value has been set(" + defaultValue + ")");
                }
                catch(FormatException)
                {
                    MessageBuilder.AddWarnMessage(reader.Name + " - Wrong value("+ reader.Value+ "). Available values: " + minNumber + "- " + maxNumber + ". The default value has been set(" + defaultValue + ")");
                } 
            }
            return returnValue;
        }

        private string CheckControl(XElement reader, string name)
        {
            string returnValue = ParityBitControl.NAME;
            if (reader == null)
            {
                MessageBuilder.AddWarnMessage(name + " has not been set. The default value has been set("+ ParityBitControl.NAME+ ")");
            }
            else
            {
                try
                {
                    string tempValue = reader.Value.ToLower();
                    if (tempValue.Equals(CheckSumControl.NAME) || tempValue.Equals(ParityBitControl.NAME) || tempValue.Equals(CRCControl.NAME))
                        returnValue = tempValue;
                    else
                        MessageBuilder.AddWarnMessage(reader.Name + " - the set control type (" + reader.Value + ") doesnt exist ( available values: " + CheckSumControl.NAME + ", " + ParityBitControl.NAME +
                            ", " + CRCControl.NAME + "). The default value has been set(" + ParityBitControl.NAME + ")");
                }
                catch (FormatException)
                {
                    MessageBuilder.AddWarnMessage(reader.Name + " - Wrong value(" + reader.Value + "). Available values: " + CheckSumControl.NAME + ", " + ParityBitControl.NAME +
                            ", " + CRCControl.NAME + "). The default value has been set(" + returnValue + ")");
                }
            }
            return returnValue;
        }


        public bool NextTransmission()
        {
            List<TransmissionData> transmissionLists =  (from e in XDocument.Load(fileName).Root.Elements("transmission")
                                where (int)e.Element("id") == this.currentId
                                select new TransmissionData
                                {
                                    interferenceLevel = CheckNumberElement<int>(e.Element("interference_level"), 10000, 0, 50, "interference_level"),
                                    sizeOfFrame = CheckNumberElement<int>(e.Element("size_of_frame"), 1024, 4, 32, "size_of_frame"),
                                    numbersOfFrameInPackage = CheckNumberElement<int>(e.Element("numbers_of_frames_in_package"), 1024, 4, 32, "numbers_of_frames_in_package"),
                                    numberOfTranssmision = CheckNumberElement<ulong>(e.Element("number_of_transsmisions"), 1024, 1, 32, "number_of_transsmisions"),
                                    sizeControlPart = CheckNumberElement<int>(e.Element("size_control_part"), 256, 1, 8, "size_control_part"),
                                    controlType = CheckControl(e.Element("control_type"), "control_type"),
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