using PackageDetection.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class ResultsStorage
    {
        public enum Data
        {
            noError = 0,
            Detected = 1,
            unDetected = 2,
            detectedNoError = 3,
            number_of_transmission = 4
        }
        // 0 - noError
        // 1 - Detected
        // 2 - unDetected
        // 3 - detectedNoError
        // 4 - number_of_transmission

        const int COUNT = 5;
        InfiniteNumber[] P_results = new InfiniteNumber[COUNT];
        InfiniteNumber[] F_results = new InfiniteNumber[COUNT];

        public ResultsStorage()
        {
            for (int i = 0; i < COUNT; i++)
            {
                P_results[i] = new InfiniteNumber();
                F_results[i] = new InfiniteNumber();
            }
        }
        public void AddResults(ulong[] packageLst, ulong[] frameLst)
        {
            PackageAddResults(packageLst);
            FrameAddResults(frameLst);
        }

        public void PackageAddResults(ulong[] lst)
        {
            for (int i = 0; i < COUNT; i++)
                P_results[i].O_Add(lst[i]);
        }
        public void FrameAddResults(ulong[] lst)
        {
            for (int i = 0; i < COUNT; i++)
                F_results[i].O_Add(lst[i]);
        }

        //public void ShowResults()
        //{
        //    Console.Clear();
        //    Console.WriteLine("------------------------------WYNIKI-------------------------------------");
        //    Console.WriteLine("------------------------------Pakiet-------------------------------------");
        //    Console.WriteLine("|Liczba pakietow bez bledu :                        " + P_results[(int)Data.noError]);
        //    Console.WriteLine("|Liczba wykrytych blednych pakietow :               " + P_results[(int)Data.Detected]);
        //    Console.WriteLine("|Liczba niewykrytych blednych pakietow :            " + P_results[(int)Data.unDetected]);
        //    Console.WriteLine("|Liczba wykrytych bledow, przy poprawnych danych:   " + P_results[(int)Data.detectedNoError]);
        //    Console.WriteLine("|Liczba transmisji -                                " + P_results[(int)Data.number_of_transmission]);
        //    Console.WriteLine("------------------------------Ramki--------------------------------------");
        //    Console.WriteLine("|Liczba ramek bez bledu :                           " + F_results[(int)Data.noError]);
        //    Console.WriteLine("|Liczba wykrytych blednych ramek :                  " + F_results[(int)Data.Detected]);
        //    Console.WriteLine("|Liczba niewykrytych blednych ramek :               " + F_results[(int)Data.unDetected]);
        //    Console.WriteLine("|Liczba wykrytych bledow, przy poprawnych danych:   " + F_results[(int)Data.detectedNoError]);
        //    Console.WriteLine("|Liczba transmisji -                                " + F_results[(int)Data.number_of_transmission]);
        //    Console.WriteLine("-------------------------------------------------------------------------");
        //}

        public void ShowResults(ref ResultsWindow resultsWin)
        {
            resultsWin.SetResultsboxes(this.P_results, this.F_results);
        }

        //public InfiniteNumber P_noError = new InfiniteNumber();
        //public InfiniteNumber P_Detected = new InfiniteNumber();
        //public InfiniteNumber P_unDetected = new InfiniteNumber();
        //public InfiniteNumber P_detectedNoError = new InfiniteNumber();
        //public InfiniteNumber P_number_of_transmission = new InfiniteNumber();

        //public InfiniteNumber F_noError = new InfiniteNumber();
        //public InfiniteNumber F_Detected = new InfiniteNumber();
        //public InfiniteNumber F_unDetected = new InfiniteNumber();
        //public InfiniteNumber F_detectedNoError = new InfiniteNumber();
        //public InfiniteNumber F_number_of_transmission = new InfiniteNumber();



    }
}
