using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass
{
    public class SineCollisionData : CollisionData
    {

        public const string NAME = "sine_collision";

        public const string X_End = "_XEnd";
        public const string X_Start = "_XStart";



        public SineCollisionData()
        {
            ArgsNames = new List<String>() { X_Start, X_End };
            this.args[X_Start] = 0;
            this.args[X_End] = 0;
            
        }

        public override void SetComponentsByXML(XElement reader)
        {
            this.name = NAME;
            this.args = (
                from p in reader.DescendantsAndSelf()
                select new Dictionary<string, int>
                {
                    { X_Start,          CheckNumberElement<int>(p.Element(X_Start), 1000, -1000, 0, X_Start) },
                    { X_End,            CheckNumberElement<int>(p.Element(X_End), 1000, -1000, 1, X_End) }
                }).ElementAt(0);
        }


    }
}
