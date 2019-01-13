using Menu_GUI;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using PackageDetection.MessageBuilderPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{

    public static class Helpers
    {
        public const int BIT_COLLISION = 0;
        public const int SINE_COLLISION = 1;
        public const int RANDOM_COLLISION = 2;
        //
        public const int FLEXIBLE = 0;

        //Generuje losowe liczby. Static zapewnia wygenerowanie innych liczb za kazdym razem.
        static Random rnd = new Random();

        /// <summary>
        /// Zamienia liste przechowywujaca typ byte na liczbe.
        /// </summary>
        /// <param name="nList"></param>
        /// <returns>Zwraca liczbowa reprezentacje listy</returns>
        static public ulong GetPartInDec(List<byte> nList)
        {
            string result = string.Join("", nList);
            ulong InDec = Convert.ToUInt64(result, 2); // TODO : dodac zabezpieczenie
            return InDec;
        }

        /// <summary>
        /// Generuje losowa wartosc typu byte(1 lub 0).
        /// </summary>
        /// <returns></returns>
        static public byte GenerateRandomByte()
        {
            return (byte)rnd.Next(0, 2);
        }

        /// <summary>
        /// Generuje losowa liczbe z przedzialu
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        static public int GenerateRandomNumber(int min, int max)
        {
            return rnd.Next(min, max+1);
        }

        /// <summary>
        /// Zamienia liczbe w systemie dziesietnym na liczbe w systemie binarnym w postaci Listy.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static public List<byte> ConvertDecToByteList(int number)
        {
            String binary = Convert.ToString(number, 2);
            List<byte> byteList = new List<byte>();
            for (int i = 0; i < binary.Length; i++)
            {
                if (Int32.TryParse(Convert.ToString(binary[i]), out int toList))
                    byteList.Add(Convert.ToByte((byte)toList));
                else
                    Console.WriteLine("String could not be parsed.");

            }
            return byteList;
        }


        public static void AddElements(List<byte> nlst, int number)
        {
            foreach (var item in Helpers.ConvertDecToByteList(number))
            {
                nlst.Add(item);
            }
        }


        public static IMenuCollision MenuCollisionFactory(int menuCollisionType)
        {
            switch (menuCollisionType)
            {
                case (BIT_COLLISION):
                    return new MenuBitsCollision();
                case (SINE_COLLISION):
                    return new MenuSineCollision();
                case (RANDOM_COLLISION):
                    return new MenuRandomCollision();
            }
            Console.WriteLine("***** Something wrong in - HELPERS.MENUCOLLISIONFACTORY");
            return new MenuBitsCollision();

        }

        public static IMenuCollision MenuCollisionFactory(string menuCollisionType)
        {
            menuCollisionType = menuCollisionType.ToLower();
            switch (menuCollisionType)
            {
                case (BitsCollisionData.NAME):
                    return new MenuBitsCollision();
                case (SineCollisionData.NAME):
                    return new MenuSineCollision();
                case (RandomCollisionData.NAME):
                    return new MenuRandomCollision();
            }
            Console.WriteLine("***** Something wrong in - HELPERS.MENUCOLLISIONFACTORY");
            return null;

        }


        public static CollisionData CollisionDataFactory(string collisionName)
        {
            collisionName = collisionName.ToLower();
            switch (collisionName)
            {
                case (BitsCollisionData.NAME):
                    return new BitsCollisionData();
                case (SineCollisionData.NAME):
                    return new SineCollisionData();
                case (RandomCollisionData.NAME):
                    return new RandomCollisionData();
            }
            Console.WriteLine("***** Something wrong in - HELPERS.CollisionDataFactory");
            return null;
            //return new CollisionTypeData(ref resultWindow, ref pSettings);

        }

        public static IControl ControlDataFactory(string controlName)
        {
            controlName = controlName.ToLower();
            switch (controlName)
            {
                case (ByteSumControl.NAME):
                    return new ByteSumControl();
                case (ParityBitControl.NAME):
                    return new ParityBitControl();
                case (CRCControl.NAME):
                    return new CRCControl();
            }
            Console.WriteLine("***** Something wrong in - HELPERS.CollisionDataFactory");
            return null;
            //return new CollisionTypeData(ref resultWindow, ref pSettings);

        }

        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }


        public static T CheckNumberElement<T>(T checkedNumber, T maxNumber, T minNumber, T defaultValue, string name)
        {
            bool isCorrect = true;
            T returnValue = (T)Convert.ChangeType(defaultValue, typeof(T));
            //XElement value = reader.Element("interference_level");
            if (checkedNumber == null)
            {
                MessageBuilder.AddErrorMessage(name + " has not been set.");
                isCorrect = false;
            }
            else
            {
                dynamic tempValue;
                try
                {
                    tempValue = checkedNumber;
                    if (tempValue >= minNumber && tempValue <= maxNumber)
                        returnValue = (T)tempValue;
                    else
                    {
                        MessageBuilder.AddErrorMessage(name + " - the set numbere(" + checkedNumber + ") is not in the range <" + minNumber + ", " + maxNumber +">.");
                        isCorrect = false;
                    }
                }
                catch (FormatException)
                {
                    MessageBuilder.AddErrorMessage(name + " - Wrong value(" + checkedNumber + "). Available values: <" + minNumber + ", " + maxNumber + ">.");
                    isCorrect = false;
                }
            }
            if (!isCorrect)
                MessageBuilder.AddWarnMessage("The default value has been set(" + defaultValue + ")");
            return returnValue;
        }
    }
}
