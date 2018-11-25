using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class ControlElements 
    {
        private List<byte> control_holder = new List<byte>(); //przechowuje bity czesci kontrolnej
        private List<byte> CRC_divider;                       // dzielnik uzywany do kontroli CRC. Tworzony w momencie ustalenia typu jako CRC
        private bool isChanged = false;                       // flaga okreslajaca czy zaszly jakies zmiany od czasu "wyslania" 



        public bool IsChanged()
        {
            return isChanged;
        }
        public void AddByte(Byte b)
        {
            control_holder.Add(b);
        }


        #region Set Methods
        public void SetCRCDivider(List<byte> nList)
        {
            CRC_divider = new List<byte>();
            foreach (var item in nList)
            {
                CRC_divider.Add(item);
            }
        }

        public void SetControlPart(int number)
        {
            control_holder.RemoveAll(item => true == true); //kasuje wszystkie elementy, zawsze
            Helpers.AddElements(control_holder, number);
        }

        public void SetControlPart(List<byte> nList)
        {
            control_holder.Clear();
            foreach (var item in nList)
            {
                control_holder.Add(item);
            }
        }
        public void SetSizeOfControlPart(int number)
        {
            if(number > 0)
            {
                if(control_holder.Count > number)
                {

                }
            }
              
        }
        #endregion

        public List<byte> GetCRCDivider()
        {
            return CRC_divider;
        }
        public int GetCount()
        {
            return control_holder.Count;
        }
        public ulong GetControlPartInDec()
        {
            return Helpers.GetPartInDec(control_holder);
        }
        public List<byte> GetList()
        {
            return control_holder;
        }
        //public int GetSizeOfControlPart() // to samo co get Count - pomysl o tym
        //{
        //    return sizeOfControlPart;
        //}

        public byte this[int number]
        {
            get
            {
                return control_holder[number];
                //throw new ArgumentOutOfRangeException();
            }
            set
            {
                try
                {
                    if (control_holder[number] != value)
                        isChanged = true;
                    control_holder[number] = value;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("wystapil blad z przypisywaniem []");
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
