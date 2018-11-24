using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{

    public class BitsCollision : ICollision
    {
        private bool isRandom = true;
        private int indexOfFirstBit;
        private int indexOfFrame;
        private int numberOfBitsToChange;
        private List<int> indexesToChange;
        private int currentIndex = 0;    
        private CollisionType basedOn = CollisionType._package;
        private Memory memo = new Memory();

        private class Memory
        {
            public bool M_isRandom;
            public int M_indexOfFirstBit;
            public int M_indexOfFrame;
            public int M_numberOfBitsToChange;
            public int M_currentIndex;
            public CollisionType M_basedOn;

            
        }

        //używane do zwracania stanu 
        private void SetMemory()
        {
            memo.M_isRandom = this.isRandom;
            memo.M_indexOfFirstBit = this.indexOfFirstBit;
            memo.M_indexOfFrame = this.indexOfFrame;
            memo.M_numberOfBitsToChange = this.numberOfBitsToChange;
            memo.M_currentIndex = this.currentIndex;
            memo.M_basedOn = this.basedOn;
        }
        private void LoadDataFromMemory()
        {
            this.isRandom = memo.M_isRandom;
            this.indexOfFirstBit = memo.M_indexOfFirstBit;
            this.indexOfFrame = memo.M_indexOfFrame;
            this.numberOfBitsToChange = memo.M_numberOfBitsToChange;
            this.currentIndex = memo.M_currentIndex;
            this.basedOn = memo.M_basedOn;

        }
        private enum CollisionType
        {
           _package = 0,
           _frame = 1
        }
        public bool IsRandom { get => isRandom; set => isRandom = value; }
        public int IndexOfFirstBit { get => indexOfFirstBit; set => indexOfFirstBit = value; }
        public class Builder
        {
            BitsCollision BC = new BitsCollision();

            /// <summary>
            /// Okresla czy biy maja byc przeklamane losowo, czy konkretny ciąg
            /// </summary>
            /// <param name="random">Okresla czy biy maja byc przeklamane losowo, czy konkretny ciąg /param>
            public Builder SetRandomCollision()
            {
                BC.IsRandom = true;
                BC.indexOfFrame = 0;
                BC.indexOfFirstBit = 0;
                return this;
            }
            /// <summary>
            /// Przeklamuje okreslona liczbe bitow w calym pakiecie
            /// </summary>
            /// <param name="numberOfFrame">Ustawia startowa ramke do przeklamania (dla kolizji grupowej )</param>
            /// <returns></returns>
            public Builder SetBasedOnPackage(bool basedOnPakcage, int numberOfFrame = 0)
            {
                if (basedOnPakcage == true)
                {
                    BC.basedOn = CollisionType._package;
                    BC.indexOfFrame = numberOfFrame;
                }
                else
                    BC.basedOn = CollisionType._frame;
                return this;
            }
            /// <summary>
            /// Dla KAZDEJ ramki pakietu przeklamuje okreslona liczbe bitow
            /// </summary>
            /// <returns></returns>
            public Builder SetBasedOnFrame()
            {
                BC.basedOn = CollisionType._frame;
                return this;
            }
            /// <summary>
            /// Przeklamuje grupe bitow, zaczynajac od firstIndex
            /// </summary>
            /// <param name="firstIndex">pierwszy index przeklamania</param>
            /// <param name="numberOfFrame">pierwsza ramka przeklamania (dla przeklaman bazowanych na calym pakiecie</param>
            /// <returns></returns>
            public Builder ChangeGroupOfBits(int firstIndex)
            {
                BC.IsRandom = false;
                BC.IndexOfFirstBit = firstIndex;
                return this;
            }
            public BitsCollision Create()
            {
                BC.SetMemory();
                return BC;
            }


        } // na potrzeby gui niestety Builder nie bedzie przydatny, dlatego konstruktor jest publiczny
        private BitsCollision(){ } // ZABEZPIECZNIE (BUILDER)
        private void GenerateRandomNumber(int quantity, int maxIndex)
        {
            var rnd = new Random();
            this.indexesToChange = Enumerable.Range(0, maxIndex).OrderBy(x => rnd.Next()).Take(quantity).ToList();
            this.indexesToChange.Sort();
        }
        private void DoRandomCollision(ref Frame _frame)
        {
            for (int i = 0; i < _frame.GetCountInformationAndControlPart(); i++, currentIndex++)
            {
                if (this.indexesToChange[0] == currentIndex)
                {
                    _frame[i] = (_frame[i] == 0 ? (byte)1 : (byte)0);
                    this.indexesToChange.RemoveAt(0);

                    this.numberOfBitsToChange--;

                    bool isEmpty = !this.indexesToChange.Any();
                    if (isEmpty)
                    {
                        break;
                    }
                }
            }
        }
        private void DoCollisionGroupOfBits(ref Frame _frame, int firstIndex = 0)
        {
            if (firstIndex > _frame.GetCountInformationAndControlPart())
            {
                Console.WriteLine("Podano index pierwszego bitu wiekszy niz rozmiar ramki/pakietu - zamieniono na index 0");
                firstIndex = 0;
            }
            else
            {
                for (int i = firstIndex; this.numberOfBitsToChange != 0; i++, this.numberOfBitsToChange--)
                {
                    if (i >= _frame.GetCountInformationAndControlPart())
                        break;  // przerwanie, jeśli index większy od maksymalnego 
                    else
                    {
                        try
                        {
                            _frame[i] = (_frame[i] == 0 ? (byte)1 : (byte)0);// ustawia zawsze wartosc przeciwna w momencie wystapienia zaklocenia
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Proba przeklamania bitow spoza przedzialu - ustawiono index na 0");
                            i = 0;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Nieznany blad przy Kolizji Bitow");
                        }
                    }
                }


            }
        }

        private void CollisionControlPartPackage( ControlElements ce)
        {
            if (this.isRandom == false)
            {
                for (int i = 0; i < ce.GetCount() && this.numberOfBitsToChange != 0; i++)
                {
                    ce[i] = (ce[i] == 0 ? (byte)1 : (byte)0);
                    this.numberOfBitsToChange--;
                }
            }
            else
            {
                for (int i = 0; i < ce.GetCount() && this.indexesToChange.Any() != false; i++, this.currentIndex++)
                {
                    if (this.indexesToChange[0] == this.currentIndex)
                    {
                        ce[i] = (ce[i] == 0 ? (byte)1 : (byte)0);
                        this.indexesToChange.RemoveAt(0);
                    }
                    this.numberOfBitsToChange--;
                }
            }

            
        }

        /// <summary>
        /// Przekłamuje bity ramki
        /// </summary>
        /// <param name="interference_level">Okresla liczbe przeklamanych bitow /param>
        public void DoCollision(Frame nFrame, int interference_level)
        {
            this.numberOfBitsToChange = interference_level; // zabezpieczenie przed uzyciem tylko tej metody
            if(this.isRandom == true)
            {
                this.DoRandomCollision(ref nFrame);
            }
            else
            {
                this.DoCollisionGroupOfBits(ref nFrame, this.indexOfFirstBit);
            }
            
        }

        /// <summary>
        /// Przekłamuje bity pakietu
        /// </summary>
        /// <param name="interference_level">Okresla liczbe przeklamanych bitow /param>
        public void DoCollision(Package nPackage, int interference_level)
        {
            if (basedOn == CollisionType._frame)             // kolizja ramek (przeklamanie takiej samej liczby bitów w kazdej ramce)
            {
                for (int i = 0; i < nPackage.GetFrames().Count; i++) // przeklamanie takiej samej liczby bitów w kazdej ramce
                {
                    this.numberOfBitsToChange = interference_level;  // liczba bitów do zamiany w kazdej ramce          !##############
                    currentIndex = 0;
                    if (this.isRandom == true)
                        GenerateRandomNumber(this.numberOfBitsToChange, nPackage.GetFrameCount());
                    DoCollision(nPackage[i], this.numberOfBitsToChange);    // liczba bitow do przeklamania w kazdej ramce taka sama
                }
            }
            else if(basedOn == CollisionType._package)       //kolizja pakietów (przekłamania bazujące na liczbie przeklamanych bitów )
            {
                this.numberOfBitsToChange = interference_level; // liczba bitów do zamiany w całym pakiecie           !##############
                if (this.isRandom == true)
                {
                    GenerateRandomNumber(this.numberOfBitsToChange, nPackage.GetCountOfBitsInPackage());
                    this.indexOfFrame = 0;
                    this.indexOfFirstBit = 0;
                }     


                for (int i = indexOfFrame; i < nPackage.GetFrames().Count && this.numberOfBitsToChange != 0; i++)  // konkretna liczba bitow do przeklamania
                {
                    DoCollision(nPackage[i], this.numberOfBitsToChange); // numberOfBitsToChange jest coraz mniejsze (przechodzi do kolejnych ramek
                    this.indexOfFirstBit = 0;                                       // az zostanie przeklamana liczba bitów założona na poczatku )
                }
                if (this.numberOfBitsToChange > 0)          // przeklamanie czesci kontrolnej pakietu, jesli zostały jeszcze bity do przeklamania
                {
                    CollisionControlPartPackage(nPackage.GetControlPart());
                }
            }
            LoadDataFromMemory(); // zapisywanie poprzedniego stanu
        }









    }
}
