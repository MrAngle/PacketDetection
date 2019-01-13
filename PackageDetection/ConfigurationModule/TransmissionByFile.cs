using Menu_GUI;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using PackageDetection.ConfigurationModule.TransmissionDataClass.Data;
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
        TransmissionData transmissionData;
        int currentId;
        string fileName;
        bool dataIsCorrect;


        public TransmissionByFile(string fileName)
        {
            this.fileName = fileName;
            this.currentId = 0;

        }

        CollisionData GetCollisionType(XElement reader)
        {
            CollisionData cd = null;
            try
            {
                if (reader == null || reader.Element("name") == null) throw new CollisionDataException("collisionType: has not been set.");

                string collisionName = ((string)reader.Element("name")).ToLower();
                if((cd = Helpers.CollisionDataFactory(collisionName)) == null) throw new CollisionDataException("collisionType.name: wrong name has been set ("+ collisionName +")");
                try
                {
                    cd.SetComponentsByXML(reader);
                }
                catch
                {
                    throw new CollisionDataException();
                }
            }
            catch(CollisionDataException c){
                MessageBuilder.AddErrorMessage("Collision type: " + c.Message);
                dataIsCorrect = false;
            }
            return cd;
        }

        private T CheckNumberElement<T>(XElement reader, T maxNumber, T minNumber, T defaultValue, string name)
        {
            T returnValue = (T)Convert.ChangeType(defaultValue, typeof(T));
            //XElement value = reader.Element("interference_level");
            if (reader == null)
            {
                MessageBuilder.AddErrorMessage(name + " has not been set.");
                MessageBuilder.AddWarnMessage(name + ": The default value has been set(" + defaultValue + ")");
            }
            else
            {
                try
                {
                    return returnValue = Helpers.CheckNumberElement<T>(Helpers.ConvertValue<T, string>(reader.Value), maxNumber, minNumber, defaultValue, name);
                }
                catch (FormatException)
                {
                    MessageBuilder.AddErrorMessage(name + " - Wrong value(" + reader.Value + "). Available values: " + minNumber + "- " + maxNumber + ".");
                }
            }
            MessageBuilder.AddWarnMessage(name + ": The default value has been set(" + defaultValue + ")");
            return returnValue;
        }


        private List<TransmissionData> GetTransmissionByID(int transmissionID)
        {
            return (from e in XDocument.Load(fileName).Root.Elements("transmission")
                    where (int)e.Element("id") == transmissionID
                    select new TransmissionData
                    {
                        name = "_" + CheckNumberElement<int>(e.Element("id"), 99999, 0, 0, "id") + "_" + CheckName(e.Element("transmission_name"), "transmission_name") + "_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss", CultureInfo.InvariantCulture),
                        interferenceLevel = CheckNumberElement<int>(e.Element("interference_level"), 10000, 0, 50, "interference_level"),
                        sizeOfFrame = CheckNumberElement<int>(e.Element("size_of_frame"), 1024, 1, 32, "size_of_frame"),
                        numbersOfFrameInPackage = CheckNumberElement<int>(e.Element("numbers_of_frames_in_package"), 1024, 1, 32, "numbers_of_frames_in_package"),
                        numberOfTranssmision = CheckNumberElement<ulong>(e.Element("number_of_transsmisions"), 10000, 1, 32, "number_of_transsmisions"),
                        sizeControlPart = CheckNumberElement<int>(e.Element("size_control_part"), 256, 1, 8, "size_control_part"),
                        controlType = CheckControl(e.Element("control_type"), "control_type"),
                        collisionType = this.GetCollisionType(e.Element("collisionType")),
                        numberOfPackagesToEnd = CheckNumberElement<ulong>(e.Element("number_of_packages_to_end"), 999999999, 1, 5000, "number_of_packages_to_end")
                        
                    }).ToList();
        }


        public bool NextTransmission()
        {
            dataIsCorrect = true;

            MessageBuilder.AddTitleMessage("++++++++++++++++++++++++++++++");
            List<TransmissionData> transmissionList = GetTransmissionByID(this.currentId);
            this.currentId++;
            if (!(transmissionList.Count < 1))
                transmissionData = transmissionList[0];
            else
                return false;
            if (!dataIsCorrect)
            {
                MessageBuilder.AddErrorMessage("!!!!! Cannot start transmission test !!!!!");
                MessageBuilder.WriteMessageToFile("[FAILED]_" + transmissionData.name);
                MessageBuilder.ClearMessage();
                return NextTransmission();
            }

            MenuHandler.MenuCollision = Helpers.MenuCollisionFactory(transmissionData.collisionType.Name);

            MessageBuilder.AddInfoMessage(" ");
            SetPackageSettings();
            MessageBuilder.AddInfoMessage("Transmission data is ready to tests: " + transmissionData.name);
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
            MenuHandler.GetMenuPackageSettings().SetPackageToEnd(transmissionData.numberOfPackagesToEnd);

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
            }
            catch (FormatException)
            {
                MessageBuilder.AddErrorMessage("Error when trying to start transmission");
                if (transmissionData.name != null)
                    MessageBuilder.WriteMessageToFile(transmissionData.name);
                else
                    MessageBuilder.WriteMessageToFile("undefined_errors");
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

        private string CheckControl(XElement reader, string name)
        {
            bool isCorrect = true;
            string returnValue = ParityBitControl.NAME;
            if (reader == null)
            {
                MessageBuilder.AddErrorMessage(name + " has not been set.");
            }
            else
            {
                try
                {
                    string tempValue = reader.Value.ToLower();
                    if (tempValue.Equals(ByteSumControl.NAME) || tempValue.Equals(ParityBitControl.NAME) || tempValue.Equals(CRCControl.NAME))
                        returnValue = tempValue;
                    else
                    {
                        MessageBuilder.AddErrorMessage(reader.Name + " - the set control type (" + reader.Value + ") doesnt exist ( available values: " + ByteSumControl.NAME + ", " + ParityBitControl.NAME +
                            ", " + CRCControl.NAME + ").");
                        isCorrect = false;
                    }
                }
                catch (FormatException)
                {
                    MessageBuilder.AddErrorMessage(reader.Name + " - Wrong value(" + reader.Value + "). Available values: " + ByteSumControl.NAME + ", " + ParityBitControl.NAME +
                            ", " + CRCControl.NAME + ").");
                    isCorrect = false;
                }
            }
            if (!isCorrect)
                MessageBuilder.AddWarnMessage("The default value has been set(" + returnValue + ")");
            return returnValue;
        }

        private string CheckName(XElement reader, string name)
        {
            bool isCorrect = true;
            string returnValue = "Default" + (new Random()).Next(0, 100000);
            if (reader == null)
            {
                MessageBuilder.AddWarnMessage(name + " has not been set.");
                isCorrect = false;
            }
            else
            {
                try
                {
                    string tempValue = reader.Value;
                    if (tempValue.Length > 150)
                    {
                        MessageBuilder.AddWarnMessage(name + " is too long. The default value has been set(" + returnValue + ")");
                        isCorrect = false;
                    }
                    else
                        returnValue = tempValue;
                }
                catch (FormatException)
                {
                    MessageBuilder.AddWarnMessage(reader.Name + " - Wrong value(" + reader.Value + "). The default value has been set(" + returnValue + ")");
                    isCorrect = false;
                }
            }
            if (!isCorrect)
                MessageBuilder.AddWarnMessage("The default value has been set(" + returnValue + ")");
            return returnValue;
        }

    }
}
