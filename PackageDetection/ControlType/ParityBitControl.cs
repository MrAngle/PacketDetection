
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    //TODO : Nieobsluzone sizeofCOntrolPart. Mozliwosc dodania algorytmow pozwalajacych na stworzenie
    // bardziej wyrafinowanych postaci kontroli bazujacej na bicie parzystosci
    public class ParityBitControl : IControl
    {
        public const string NAME = "parity_bit";

        public List<byte> CalculateControlPart(Frame nFrame, int sizeOfControlPart = Helpers.FLEXIBLE)
        {
            ulong results = (ulong)nFrame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            return DeterminateParityBit(results);
        }
        public List<byte> CalculateControlPart(Package nPakiet, int sizeOfControlPart = Helpers.FLEXIBLE)
        {
            ulong sum = 0;
            List<byte> nLists_information_part = new List<byte>();
            foreach (var frame in nPakiet.GetFrames())
            {
                sum += (ulong)frame.GetInformationPart().Sum(x => Convert.ToInt32(x));
            }
            return DeterminateParityBit(sum);
        }
        private List<byte> DeterminateParityBit(ulong sum)
        {
            // Bit parzystosci zapisany
            List<byte> parityBit = new List<byte>();
            if (sum % 2 == 0) 
                parityBit.Add(0); // jesli suma w czesci kontrolnej jest podzielna przez 2 => bit parzystosci = 1
            else
                parityBit.Add(1); // jesli suma w czesci kontrolnej nie jest podzielna przez 2 => bit parzystosci = 0
            return parityBit;
        }

        public byte CollisionDetection(Frame nFrame)
        {
            //Zlicza jedynki z infoParty i controlPart
            ulong count = (ulong)nFrame.GetInformationPart().Count(b => b == 1) + nFrame.GetControlPart().GetControlPartInDec(); 

            return DeterminateResults(count, nFrame.IsChanged());
        }

        public byte CollisionDetection(Package nPakiet)
        {
            ulong count = nPakiet.GetControlPart().GetControlPartInDec();

            //Console.WriteLine("Count przed dodaniem ramek: " + count);          // Do usuniecia!

            foreach (var frame in nPakiet.GetFrames())                          //Zlicza jedynki z infoParty calego pakietu
            {
                count += (ulong)frame.GetInformationPart().Count(b => b == 1);  //Zlicza jedynki z infoParty  z pojedynczej ramki
            }

            //Console.WriteLine("Count - " + count);  //Do wykasowania linijka
            return DeterminateResults(count, nPakiet.IsChanged());


        }
        private byte DeterminateResults(ulong count, bool changed)
        {
            if (count % 2 == 0)                     // Sprawdzanie bledu
            {
                if (changed == false)
                {
                    //Console.WriteLine("Wyglada na to ze jest ok");              // NIe wykryto bledu i blad nie wystapil
                    return 0;
                }
                else
                {
                    //Console.WriteLine("Blad istnieje nie zostal wykryty");      // Blad istnieje i nie zostal wykryty
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
                    Console.WriteLine(changed + " zmiana ");
                    Console.WriteLine("Bledne wykrycie - COS JEST NIE TAK ---------------------------------------------------");                       // Bledne wykrycie
                    return 3;
                }
            }
        }

    }

}
