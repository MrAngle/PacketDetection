using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    /* Realizacja Klasy Frame
     * 
     * 
     * Przykład:
     * newFrame1
     * Część Informacyjna        : Kontrolna
     * [0|1|1|1|0|1] : [1|0|1|1]
     * 
     * Część kontrolna jest jako osobny objekt i można się do niej odwołać za pomocą funkcji List<byte> GetControlPart()
     * 
     * Do konkretnych elementów ramki można się dostać w następujący sposób:
     * 
     * newFrame1[0] - zwróci pierwszy element listy częsci informacyjnej ( w tym przypadku 0 )
     * newFrame1[5] - zwróci szósty element listy części informacyjnej ( czyli 0 )
     * newFrame1[7] - zwróci drugi element czesci kontrolnej!!!!                                  <------------
     * newFrame1.getControlPart()[0] - zwróci pierwszy element części kontrolnej ramki ( czyli 1 )
     * newFrame1.getControlPart()[1] - zwróci drugi element części kontrolnej ramki ( czyli 0 )
     * 
     * Przeciążony jest operator [] !!
     * 
     * ==============================================================================
     * Przykładowy sposob tworzenia ramki:
     * Frame fra = new Frame.Builder().RandomFrame(30).SetControl(7).Create()
     * 
     * (Opis poszczegolnych metod przy deklaracji)
     * ==============================================================================
     */

    public class Frame 
    {
        private List<byte> frame_holder = new List<byte>();
        private ControlElements control_part = new ControlElements();
        private IControl control_type;
        private bool isChanged = false;

        #region Private zone


        private void Show(List<byte> array)
        {
            foreach (var item in array)
            {
                Console.Write(item);
            }
            //Console.WriteLine();
        }


        private Frame()
        {
            // Ochrona przed tworzeniem obiektu bez buildera
        }
        #endregion

        #region Builder zone
        public class Builder
        {
            Frame fra = new Frame();
            /// <summary>
            /// Tworzy ramke z losowo wygenerowanymi bitami.
            /// </summary>
            /// <param name="size">Okresla dlugosc ramki</param>
            public Builder RandomFrame(int size)
            {
                fra.frame_holder.RemoveAll(item => true == true);
                fra.CreateRandomInformationPart(size);
                return this;
            }
            /// <summary>
            /// Tworzy ramke przechowywyjaca podana liczbe jako argument w postaci binarnej. Rozmiar ramki zalezny od podanej liczby
            /// </summary>
            /// <param name="number">Liczba okreslajaca informacyjna czesc ramki</param>
            public Builder SetFrame(int number)
            {
                fra.SetInformationPart(number);
                return this;
            }
            public Builder SetFrame(List<byte> nList)
            {
                fra.frame_holder.Clear();
                foreach (var item in nList)
                {
                    fra.frame_holder.Add(item);
                }
                return this;
            }
            /// <summary>
            /// Wpisuje podana liczbe w postaci binarnej jako czesc kontrolna ramki
            /// </summary>
            /// <param name="number">Liczba okreslajaca kontrolna czesc ramki</param>
            public Builder SetControl(int number)
            {
                fra.GetControlPart().SetControlPart(number);
                return this;
            }
            public Builder SetControl(List<byte> nList)
            {
                fra.GetControlPart().SetControlPart(nList);
                return this;
            }
            /// <summary>
            /// Ustawia czesc kontrolna ramki, bazujac na typie kontroli. Przypisuje dany typ kontroli do ramki
            /// </summary> 
            /// <param name="number">Liczba okreslajaca kontrolna czesc ramki</param>
            /// <param name="sizeOfControlPart">Okresla rozmiar czesci kontrolnej (0 okresla dostosowanie rozmiaru czesci kontrolnej w zaleznosci od dlugosci ramki)</param>
            public Builder SetControlType(IControl control_type, int sizeOfControlPart = Functions.FLEXIBLE)
            {
                fra.SetControlType(control_type);
                fra.SetControlPartByType(sizeOfControlPart);
                return this;
            }
            /// <summary>
            /// Zwraca ramke wygenerowana przez poprzednie metody
            /// </summary>
            public Frame Create()
            {
                return fra;
            }
        }
        #endregion


        ~Frame()
        {
            //Console.WriteLine("Kasuje");
        }
        public void CreateRandomInformationPart(int size)
        {
            for (int i = 0; i < size; i++)
                frame_holder.Add(Functions.GenerateRandomByte());
        }

        /// <summary>
        /// Zwraca informacje czy jakis element ramki zostal zamieniony
        /// </summary>
        /// <returns></returns>
        public bool IsChanged()
        {
            if (control_part.IsChanged() == true)
                isChanged = true;
            return isChanged;
        }

        /// <summary>
        /// Sprawdza czy ramka zostala przeklamana oraz okresla czy zostalo to wykryte przez czesc kontrolna
        /// </summary>
        /// <returns></returns>
        public byte CheckFrame()
        {
            return control_type.CollisionDetection(this);
        }

        #region Show Methods
        public void ShowInformationPart()
        {
            //Console.Write("Information part : ");
            Show(frame_holder);
        }
        public void ShowControlPart()
        {
            //Console.Write("Control part : ");
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
        #endregion

        #region Get Methods

        /// <summary>
        /// NIE DZIALAJACA METODA!@!
        /// </summary>
        /// <returns></returns>
        public List<byte> GetCopyInformationAndControlPart()
        {
            //TODO : Znalezc sposob na prawidlowe zwracanie referencji części kontrolnej i informacyjnej
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zwraca czesc informacyjna ramki
        /// </summary>
        /// <returns></returns>
        public List<byte> GetInformationPart()
        {
            return frame_holder;
        }
        /// <summary>
        /// Zwraca czesc informacyjna ramki w jako liczbe
        /// </summary>
        /// <returns></returns>
        public ulong GetInformationPartInDec()
        {
            return Functions.GetPartInDec(this.GetInformationPart());
        }
        public ControlElements GetControlPart()
        {
            return control_part;
        }

        /// <summary>
        /// Zwraca liczbe elementow zawartych w sumie w czesci informacyjnej oraz kontrolnej
        /// </summary>
        /// <returns></returns>
        public int GetCountInformationAndControlPart()
        {
            return frame_holder.Count + control_part.GetList().Count;
        }

        
        #endregion

        #region Set Methods
        public void SetInformationPart(int number)
        {
            frame_holder.RemoveAll(item => true == true);
            Functions.AddElements(control_part.GetList(), number);
        }
        

        public void SetControlPartByType(int sizeOfControlPart = Functions.FLEXIBLE)
        {
            try
            {
                control_part.SetControlPart(control_type.CalculateControlPart(this, sizeOfControlPart));
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu - SetControlPartByType");
            }
        }
        public void SetControlType(IControl ICon)
        {
            try
            {
                control_type = ICon;
            }
            catch(System.NullReferenceException)
            {
                Console.WriteLine("Nie zdefiniowany typ obiektu - SetControlType");
            }
        }

        
        #endregion

        public byte this[int number]
        {
            get
            {
                if (number >= frame_holder.Count) // gdy podawana jest liczba spoza zakresu automatycznie przechodzi do czesci kontrolnej
                {
                    try
                    {
                        return control_part[number - frame_holder.Count];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("wystapil blad z wyciaganiem z []");
                       
                        throw new ArgumentOutOfRangeException();
                    }
                }
                try
                {
                    return frame_holder[number];
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("wystapil blad z wyciąganiem z []");
                    throw new ArgumentOutOfRangeException();
                }
            }
            set
            {

                if (number >= frame_holder.Count) //TODO: Sprawdz dzialanie!!!!!!!!!!!!!!!!!!!!
                {
                    try
                    {
                        control_part[number - frame_holder.Count] = value;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("wystapil blad z przypisywaniem []");
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    try
                    {
                        if (frame_holder[number] != value)
                            isChanged = true;
                        frame_holder[number] = value;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("wystapil blad z przypisywaniem []");
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        #region Equal Methods
        public static bool operator ==(Frame a1, Frame a2)
        {
            return a1.IsEqual(a2);
        }
        public static bool operator !=(Frame a1, Frame a2)
        {
            return !(a1.IsEqual(a2));
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Frame))
            {
                if ((Frame)obj == this)
                    return true;
            }

            return false;
        }

        public bool IsEqual(Frame ao)
        {
            for (int i = 0; i < ao.GetCountInformationAndControlPart(); i++)
            {
                if (ao[i] != this[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 1055582354;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<byte>>.Default.GetHashCode(frame_holder);
            hashCode = hashCode * -1521134295 + EqualityComparer<ControlElements>.Default.GetHashCode(control_part);
            hashCode = hashCode * -1521134295 + EqualityComparer<IControl>.Default.GetHashCode(control_type);
            hashCode = hashCode * -1521134295 + isChanged.GetHashCode();
            hashCode = hashCode * -1521134295 + this.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}
