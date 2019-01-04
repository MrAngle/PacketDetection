using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class CheckSumControl : IControl
    {
        public const string NAME = "check_sum";
        
        /// <summary>
        /// Oblicza czesc kontrolna dla ramki
        /// </summary>
        /// <param name="nFrame"></param>
        /// <param name="sizeOfControlPart"></param>
        /// <returns></returns>
        public List<byte> CalculateControlPart(Frame nFrame, int sizeOfControlPart = Helpers.FLEXIBLE)
        {
            int results = nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));     // zlicza jedynki w ramce
            List<byte> CheckSum = Helpers.ConvertDecToByteList(results);              // tworzy czesc kontrolna na 
                                                                                        // podstawie wyliczonej sumy

            if (sizeOfControlPart != Helpers.FLEXIBLE)        // sprawdza czy czesc kontrolna ma miec okreslona dlugosc        
                SetSizeOfList(ref CheckSum, sizeOfControlPart); // ? ustawia czesc kontrolna na konkretna dlugosc

            return CheckSum;
        }

        /// <summary>
        /// Oblicza czesc kontrolna dla pakietu
        /// </summary>
        /// <param name="nPackage"></param>
        /// <param name="sizeOfControlPart"></param>
        /// <returns></returns>
        public List<byte> CalculateControlPart(Package nPackage, int sizeOfControlPart = Helpers.FLEXIBLE)
        {
            int sum = 0; 
            foreach (var item in nPackage.GetFrames()) 
            {
                sum += (int)item.GetInformationPart().Sum(x => Convert.ToInt32(x));
                sum += (int)item.GetControlPart().GetList().Sum(x => Convert.ToInt32(x)); // sumuje czesci kontrolne wszystkich ramek
            }
            List<byte> CheckSum = Helpers.ConvertDecToByteList(sum);   // tworzy czesc kontrolna na podstawie wyliczonej sumy
            //Console.WriteLine(CheckSum);
            if (sizeOfControlPart != Helpers.FLEXIBLE)                 // sprawdza czy czesc kontrolna ma miec okreslona dlugosc
                SetSizeOfList(ref CheckSum, sizeOfControlPart); // ? ustawia czesc kontrolna na konkretna dlugosc

            return CheckSum;
        }

        /// <summary>
        /// Ustawia liczbe elementow zawartych w liscie
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="sizeOfControlPart"></param>
        public void SetSizeOfList(ref List<byte> lst, int sizeOfControlPart)
        {
           
            if (sizeOfControlPart < lst.Count)
                for (int i = lst.Count; i != sizeOfControlPart; i--)
                {
                    lst.RemoveAt(0); // zmniejszanie liczby elementow
                }
            else
                for (int i = lst.Count; i != sizeOfControlPart; i++)
                {
                    lst.Insert(0, 0); // zwiekszanie liczby elementow i ustawianie na 0
                }
           
        }


        /// <summary>
        /// Wykrywanie bledow w ramce
        /// </summary>
        /// <param name="nFrame"></param>
        /// <returns></returns>
        public byte CollisionDetection(Frame nFrame)
        {
            // moze byc int, bo maksymalna liczba jedynek w sytuacji gdy jest 10 000 * 10 000 * 8 = 800 000 000
            int results = (int)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));        // Obliczamy sume z czesci informacyjnej
            List<byte> CheckSum = Helpers.ConvertDecToByteList(results);              // tworzy czesc kontrolna na 

            if (nFrame.GetControlPart().GetCount() < CheckSum.Count)        // sprawdza czy czesc kontrolna ma miec okreslona dlugosc        
                SetSizeOfList(ref CheckSum, nFrame.GetControlPart().GetCount()); // ? ustawia czesc kontrolna na konkretna dlugosc

            results = CheckSum.Sum(x => Convert.ToInt32(x));

            // Okreslanie wynikow uzyskanych przez weryfikacje danych
            return DeterminateResults((ulong)results, nFrame.IsChanged(), (ulong)nFrame.GetControlPart().GetList().Sum(x => Convert.ToInt32(x)) ); 
        }

        public byte CollisionDetection(Package nPackage)
        {
            int results = 0;                                                      // Tworzymy zmienna pomocnicza
            foreach (var item in nPackage.GetFrames())                              // Dodajemy wszysktie czesci kontrolne ramek
            {
                results += (int)item.GetInformationPart().Sum(x => Convert.ToInt32(x));
                results += (int)item.GetControlPart().GetList().Sum(x => Convert.ToInt32(x));     // Porownujemy sume z czescia kontrolna pakietu
            }

            List<byte> CheckSum = Helpers.ConvertDecToByteList(results);              // tworzy czesc kontrolna na 

            if (nPackage.GetControlPart().GetCount() < CheckSum.Count)        // sprawdza czy czesc kontrolna ma miec okreslona dlugosc        
                SetSizeOfList(ref CheckSum, nPackage.GetControlPart().GetCount()); // ? ustawia czesc kontrolna na konkretna dlugosc

            results = CheckSum.Sum(x => Convert.ToInt32(x));



            // Okreslanie wynikow uzyskanych przez weryfikacje danych
            return DeterminateResults((ulong)results, nPackage.IsChanged(), (ulong)nPackage.GetControlPart().GetList().Sum(x => Convert.ToInt32(x)));
        }

        private byte DeterminateResults(ulong sum, bool changed, ulong object_sum)
        {
            if (sum == object_sum)                     // Sprawdzanie bledu
            {
                if (changed == false)
                {
                    //Console.WriteLine("Wyglada na to ze jest ok");              // NIe wykryto bledu i blad nie wystapil
                    return 0;
                }
                else
                {
                   // Console.WriteLine("Blad istnieje nie zostal wykryty");      // Blad istnieje i nie zostal wykryty
                    return 2;
                }
            }
            else
            {
                if (changed == true)
                {
                    //Console.WriteLine("Wykryto blad");                                 // Blad wystapil
                    return 1;
                }
                else
                {
                    //Console.WriteLine(changed + " zmiana ");
                    //Console.WriteLine("Bledne wykrycie - COS JEST NIE TAK ---------------------------------------------------");                       // Bledne wykrycie
                    return 3;
                }
            }
        }
    }
}