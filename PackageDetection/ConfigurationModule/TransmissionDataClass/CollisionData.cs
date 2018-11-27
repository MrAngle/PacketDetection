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

        protected List<string> ArgsNames;
        protected Dictionary<string, int> args = new Dictionary<string, int>();

        public Dictionary<string, int> Args { get => args; }

        public abstract void SetComponentByName(string componentName, string value);

        public abstract void SetComponentsByXML(XElement reader);

        
    }

}
