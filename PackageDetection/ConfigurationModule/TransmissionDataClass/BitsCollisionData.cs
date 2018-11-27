using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass
{
    public class BitsCollisionData : CollisionData
    {

        public const string NAME = "bits_collision";
        private const string IS_RANDOM_CHECKBOX = "_israndom";
        private const string FIRST_INDEX = "_firstindex";
        private const string FIRST_FRAME = "_firstframe";


        public BitsCollisionData()
        {
            ArgsNames = new List<String>() { IS_RANDOM_CHECKBOX, FIRST_INDEX, FIRST_FRAME };
            this.args[IS_RANDOM_CHECKBOX] = 0;
            this.args[FIRST_INDEX] = 0;
            this.args[FIRST_FRAME] = 0;
        }

        public override void SetComponentByName(string componentName, string value)
        {
            
            string componentNameL = componentName.ToLower();
            string valueL = value.ToLower();

            var match = ArgsNames.FirstOrDefault(stringToCheck => stringToCheck.Contains(componentName));

            //switch (componentNameL)
            //{
            //    case (IS_RANDOM_CHECKBOX):
            //        _IsRandom.SetCurrentValue(CheckBox.IsCheckedProperty, valueL.Equals("true") ? true : false);
            //        break;
            //    case (FIRST_FRAME):
            //        //TODO : Dodac zabezpiecznia
            //        _FirstFrame.Text = value;
            //        break;
            //    case (FIRST_INDEX):
            //        _FirstIndex.Text = value;
            //        break;
            //}
        }

        public override void SetComponentsByXML(XElement reader)
        {
            this.args = (
                from p in reader.Elements("collisionType")
                select new Dictionary<string, int>
                {
                    { FIRST_INDEX,          (int)p.Element(FIRST_INDEX) },
                    { FIRST_FRAME,          (int)p.Element(FIRST_FRAME) },
                    { IS_RANDOM_CHECKBOX,   (int)p.Element(IS_RANDOM_CHECKBOX)}
                }).ElementAt(0);

            foreach (var item in Args)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }
        }

    }
}
