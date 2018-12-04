using PackageDetection.MessageBuilderPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass.Data
{

    public class CollisionDataException : Exception
    {
        public CollisionDataException()
        {
            WriteToMessageBuilder("Collision data exception - something goes wrong :)) ");
        }

        public CollisionDataException(string message)
            : base(message)
        {
            WriteToMessageBuilder(message);
        }

        public CollisionDataException(string message, Exception inner)
            : base(message, inner)
        {
            WriteToMessageBuilder(message);
        }

        private void WriteToMessageBuilder(string message)
        {
            MessageBuilder.AddErrorMessage(message);
        }
    }
}
