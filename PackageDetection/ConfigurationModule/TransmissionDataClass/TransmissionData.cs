
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass
{
    public struct TransmissionData
    {
        public int interferenceLevel;
        public int sizeOfFrame;
        public int numbersOfFrameInPackage;
        public ulong numberOfTranssmision;
        public int sizeControlPart;
        public string controlType; //TODO: zmienic na IControl
        public CollisionData collisionType;
    }


   

    
}
