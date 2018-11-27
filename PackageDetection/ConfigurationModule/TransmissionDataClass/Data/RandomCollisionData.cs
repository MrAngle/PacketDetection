using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass
{
    public class RandomCollisionData : CollisionData
    {
        public const string NAME = "random_collision";


        public RandomCollisionData()
        {

        }

        public override void SetComponentsByXML(XElement reader)
        {
            this.name = NAME;
        }
    }
}
