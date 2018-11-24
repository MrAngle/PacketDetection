using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class InfiniteNumber
    {
        List<ulong> numberHolder = new List<ulong>();

        //Ulong Maxvalue = 18446744073709551615
        //Pomysl zapisu liczby nieskonczonej, polega na przypisywaniu
        //kolejnych liczb do kolejnych elementów listy, przechowywującej jedynie 
        //liczbe 10^20 wystąpien 
        //Przyklad: number = 1000000000000000015
        //numberHolder[0] = 15
        //numberHolder[1] = 1
        //MaxNumber ma o jedno zero mniej(niz rzeczywista maksymalna wartosc ulong),
        //w celu zabezpieczenia przed ewentualnymi dodawaniami spoza zakresu
        //np: 9999999999999999999 + 9999999999999999999 ( wyzerowanie ulong )


        private const ulong MaxNUMBER = 1000000000000000000;

        public InfiniteNumber(ulong number = 0)
        {
            numberHolder.Add(number);
        }


        /// <summary>
        /// Sprawdza czy w ktoryms elemencie listy wystepuje liczba wieksza od 
        /// MaxNUMBER, jesli tak, to zostanie dodana jedynka do kolejnego indexu,
        /// lub zostanie utworzony nowy element.
        /// </summary>
        /// <param name="index"></param>
        private void Transformation(int index)
        {
            try
            {
                if (index == numberHolder.Count - 1)
                    numberHolder.Add(0);

                numberHolder[index + 1]++;
                numberHolder[index] -= MaxNUMBER;
            }
            catch(ArgumentException)
            {
                Console.WriteLine("ArgumentException : Blad przy Transforamcji");
            }
        }

        /// <summary>
        /// Sprawdza czy ktoras z liczb listy przekracza MAXValue
        /// </summary>
        private void CheckNumber()
        {
            for (int i = numberHolder.Count-1; i >= 0; i--)
            {
                if (numberHolder[i] >= MaxNUMBER) // sprawdza czy została przekroczona wartosc maksymalna dla jednego elementu listy
                    this.Transformation(i); // Zamienia wartosc na konkretnej pozycji listy
                        

            }
        }




        /// <summary>
        /// Dodaje liczbe nie wieksza niz 1000000000000000000. 
        /// </summary>
        /// <param name="number"></param>
        public void O_Add(ulong number)
        {
            // Na potrzeby programu nie jest konieczne obsluzenie dodawania wiekszzych liczb
            if (number < MaxNUMBER && number >= 0)
                numberHolder[0] += number; //MAksymalna liczba jaka mozna dodac do liczby nieskonczonej to MaxNumber
            else
                Console.WriteLine("Proba dodania za dużej liczby ");
            CheckNumber();


        }

        public override string ToString()
        {
            string str = "";
            try
            { 
                
                str += numberHolder[numberHolder.Count - 1]; //pierwsza liczba bez dodatkowych 0
                //Console.WriteLine(str);
                for (int i = numberHolder.Count - 2; i >= 0; i--)
                {
                    string str2 = "";
                    str2 += numberHolder[i];
                    Console.WriteLine( str2.Count());

                    for (int j = str2.Count(); j < 18; j++)
                    /*
                    * dlugosc kolejnych liczb musi byc rowna 19
                    * w przypadku nie spelnienia tego warunku wygenerowane
                    * zostaja 0
                    */
                    {
                        str2 = str2.Insert(0, "0");
                    }
                    
                    str += str2;
                }
            }
            catch(ArgumentException)
            {
                Console.WriteLine("ArgumentException : Blad ToString ");
            }
            return str;
        }
    }
}
