using PackageDetection.ConfigurationModule.TransmissionDataClass.Data;
using PackageDetection.MessageBuilderPackage;
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass
{
    public abstract class CollisionData
    {

        protected string name;
        protected List<string> ArgsNames;
        protected Dictionary<string, int> args = new Dictionary<string, int>();

        public Dictionary<string, int> Args { get => args; }
        public string Name { get => name; set => name = value; }

        public abstract void SetComponentsByXML(XElement reader);

        protected T CheckNumberElement<T>(XElement reader, T maxNumber, T minNumber, T defaultValue, string name)
        {
            T returnValue = (T)Convert.ChangeType(defaultValue, typeof(T));
            //XElement value = reader.Element("interference_level");
            if (reader == null)
            {
                throw new CollisionDataException(name + ": has not been set!");
            }
            else
            {
                try
                {
                    return returnValue = Helpers.CheckNumberElement<T>(Helpers.ConvertValue<T, string>(reader.Value), maxNumber, minNumber, defaultValue, name);
                }
                catch (FormatException)
                {
                    MessageBuilder.AddErrorMessage(name + " - Wrong value(" + reader.Value + "). Available values: <" + minNumber + ", " + maxNumber + ">");
                }
            }
            MessageBuilder.AddWarnMessage(name + ": The default value has been set(" + defaultValue + ")");
            return returnValue;
        }
    }

}
