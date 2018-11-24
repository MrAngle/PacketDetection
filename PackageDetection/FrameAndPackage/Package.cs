using System;
using System.Collections.Generic;




/*
 * Pakiet jako przechowywacz listy Ramek
 * 
 * Przyklad ( losowe generowanie Ramek za pomoca GenerateFrameList() )
 * R - ramka
 * 
 * [ Pakiet1 ]
 * Lista ramek             : czesc kontrolna
 * [R0][R1][R2][R3][R4]... : [1][0][1]
 * 
 * Pakiet1[0] - pierwsza ramka 
 * Pakiet1[2] - trzecia ramka
 * 
 * Czesc kontrolna nie jest polaczona z Lista ramek.
 * Do czesci kontroli mozna sie dostac za pomoca :
 * pak.GetControlPart()[1] - zwroci 0 w tym przypadku
 * pak.GetControlPart()[0] - zwroci 1 w tym przypadku
 */


namespace Projekt_Kolko
{


    public class Package
    {
        private List<Frame> FrameList = new List<Frame>(); //przechowuje liste ramek
        private ControlElements control_part = new ControlElements(); // czesc kontrolna pakietu
        private IControl control_type; // typ kontroli -> ustawia czesc kontrolna
        private bool isChanged = false;

        #region Private_zone
        private void Show(List<byte> nlist)
        {
            foreach (var item in nlist)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }
        private void AddElements(List<byte> array, int number)
        {
            String binary = Convert.ToString(number, 2);
            int toList;
            for (int i = 0; i < binary.Length; i++)
            {
                if (Int32.TryParse(Convert.ToString(binary[i]), out toList))
                    array.Add(Convert.ToByte((byte)toList));
                else
                    Console.WriteLine("String could not be parsed.");

            }
        }
        #endregion

        /// <summary>
        /// Dodaje ramke na koniec listy
        /// </summary>
        public void AddFrame(Frame item)
        {
            try
            {
                FrameList.Add(item);
            }
            catch (Exception)
            {
                Console.WriteLine("Blad przy dodawaniu Ramki do Pakietu!");
            }
        }
        /// <summary>
        /// Usuwa WSZYSTKIE ramki.
        /// </summary>
        public void DeleteFrames()
        {
            FrameList.Clear();
        }

        /// <summary>
        /// Tworzy liste ramek(bez czesci kontrolnej)
        /// </summary>
        /// <param name="numberOfFrames">Liczba ramek do stworzenia</param>
        /// <param name="size">Dlugosc kazdej ramki ( bez czesci kontrolnej )</param>
        /// <param name="controlType">Typ kontroli ( domyslnie ustawiony na ParityBitControl )</param>
        /// <param name="sizeOfControlPart">Okresla dlugosc czesci kontrolnej w bitach</param>
        /// <param name="random">#NIE DZIALA# Domyslnie tworzy losowe elementy w ramkach. </param>
        public void GenerateFrameList(int numberOfFrames, int size, IControl _controlType = null, int sizeOfControlPart = Functions.FLEXIBLE, bool random = true)
        {
            if (_controlType == null)
            {
                _controlType = new ParityBitControl();
            }
            DeleteFrames();
            for (int i = 0; i < numberOfFrames; i++)
            {
                AddFrame(new Frame.Builder().RandomFrame(size).SetControlType(_controlType, sizeOfControlPart).Create());
            }
            this.SetControlType(_controlType);
            this.SetControlPartByType(sizeOfControlPart);



        }

        /// <summary>
        /// Sprawdza i zwraca bool informujacy czy zostal zmieniony jakis bit w calym pakiecie 
        /// </summary>
        /// <returns></returns>
        public bool IsChanged()
        {
            if (control_part.IsChanged() == true)
                isChanged = true;
            else
            {
                foreach (var item in FrameList)
                {
                    if (item.IsChanged() == true)
                    {
                        isChanged = true;
                        break;
                    }
                }
            }
            return isChanged;
        }

        /// <summary>
        /// Bada za pomoca typu kontroli czy pakiet zostal przeklamany.
        /// </summary>
        /// <returns></returns>
        public byte CheckPackage()
        {
            return control_type.CollisionDetection(this);
        }
        
        /// <summary>
        /// NIE DZIALA!!!! NIE TWORZY KOPII DZIALA NA TYCH SAMYCH LISTACH
        /// </summary>
        /// <returns></returns>
        public Package Copy() 
        {
            Package newPackage = new Package();
            newPackage.SetControlType(this.control_type);
            for (int i = 0; i < this.GetFrames().Count; i++)
            {
                newPackage.AddFrame(this[i]);
            }
            newPackage.GetControlPart().SetControlPart(this.GetControlPart().GetList());

            return newPackage;
        }

        #region SET METHODS
        /// <summary>
        /// Wylicza czesc kontrolna Pakietu ze wzgledu na typ kontroli
        /// </summary>
        /// <param name="sizeOfControlPart">Okresla liczbe bitow czesci kontrolnej</param>
        public void SetControlPartByType(int sizeOfControlPart = Functions.FLEXIBLE)
        {
            try
            {
                control_part.SetControlPart(control_type.CalculateControlPart(this, sizeOfControlPart));
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu");
            }
        }
        /// <summary>
        /// Ustawia typ kontroli 
        /// </summary>
        /// <param name="ICon">Typ kontroli</param>
        public void SetControlType(IControl ICon)
        {
            try
            {
                control_type = ICon;
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu");
            }
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Zwraca liste zawierajaca bity kontroli
        /// </summary>
        /// <returns></returns>
        public ControlElements GetControlPart()
        {
            return control_part;
        }
        /// <summary>
        /// Zwraca liste ramek
        /// </summary>
        /// <returns></returns>
        public List<Frame> GetFrames()
        {
            return FrameList;
        }

        /// <summary>
        /// Zwraca liczbe bitow w calym pakiecie razem z czescia kontrolna
        /// </summary>
        /// <returns></returns>
        public int GetCountOfBitsInPackage()
        {
            int count = 0;
            foreach (var item in FrameList)
            {
                count += item.GetCountInformationAndControlPart();
            }
            count += GetControlPart().GetCount();
            return count;
        }

        /// <summary>
        /// Zwraca liczbe bitow w ramce
        /// </summary>
        /// <returns></returns>
        public int GetFrameCount()
        {
            return FrameList[0].GetCountInformationAndControlPart();
        }
        #endregion

        #region Show Function
        /// <summary>
        /// Wyświetla czesc kontrolna w postaci binarnej
        /// </summary>
        /// <returns></returns>
        public void ShowControlPart()
        {
            Console.Write("Control part : ");
            Show(control_part.GetList());
            try
            {
                if (control_type.GetType() == typeof(CRCControl))
                {
                    Console.Write("CRC divider: ");
                    Show(control_part.GetCRCDivider());
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Odwolanie do nieistniejacego obiektu");
            }
        }
        /// <summary>
        /// Wyświetla wszystkie ramki w postaci binarnej
        /// </summary>
        /// <returns></returns>
        public void ShowFrames()
        {
            foreach (var item in FrameList)
            {
                item.ShowInformationPart();
                Console.Write(" ");
                item.ShowControlPart();
                Console.Write(" ");
            }
        }
        #endregion

        #region Comparison 
        public static bool operator ==(Package a1, Package a2)
        {
            return a1.IsEqual(a2);
        }
        public static bool operator !=(Package a1, Package a2)
        {
            return !(a1.IsEqual(a2));
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Package))
            {
                if ((Package)obj == this)
                    return true;
            }

            return false;
        }

        private bool IsEqual(Package ao)
        {
            for (int i = 0; i < ao.GetFrames().Count; i++)
            {
                if (ao[i] != this[i])
                    return false;
            }
            for (int i = 0; i < ao.GetControlPart().GetCount(); i++)
                if (ao.GetControlPart()[i] != this.GetControlPart()[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 1012944503;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Frame>>.Default.GetHashCode(FrameList);
            hashCode = hashCode * -1521134295 + EqualityComparer<ControlElements>.Default.GetHashCode(control_part);
            hashCode = hashCode * -1521134295 + EqualityComparer<IControl>.Default.GetHashCode(control_type);
            hashCode = hashCode * -1521134295 + isChanged.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Frame>.Default.GetHashCode(this.FrameList[0]);
            return hashCode;
        }
        #endregion


        public Frame this[int number]
        {
            get
            {
                try
                {
                    return FrameList[number];
                }
                catch (ArgumentOutOfRangeException)
                {
                    //return FrameList[0]
                    // Ewetualna obsluga wyjatku, jednak na potrzeby algorytmow moga byc przydatniejsze inne rozwiazania
                
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        
    }





}
