using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Projekt_Kolko.ResultsStorage;
//using WpfApp3.kla;

namespace PackageDetection.Results
{
    /// <summary>
    /// Logika interakcji dla klasy ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Page
    {

    
        private void ClearResults()
        {

            void act()
            { _PnoError.Text = " "; }
            _PnoError.Dispatcher.Invoke(act);
            _PDetected.Dispatcher.Invoke(act);
            _PunDetected.Dispatcher.Invoke(act);
            _PdetectedNoError.Dispatcher.Invoke(act);
            _PnumberOfTransmission.Dispatcher.Invoke(act);

            _FnoError.Dispatcher.Invoke(act);
            _FDetected.Dispatcher.Invoke(act);
            _FunDetected.Dispatcher.Invoke(act);
            _FdetectedNoError.Dispatcher.Invoke(act);
            _FnumberOfTransmission.Dispatcher.Invoke(act);


            //_PnoError.Text = " ";
            //_PDetected.Text = " ";
            //_PunDetected.Text = " ";
            //_PdetectedNoError.Text = " ";
            //_PnumberOfTransmission.Text = " ";

            //_FnoError.Text = " ";
            //_FDetected.Text = " ";
            //_FunDetected.Text = " ";
            //_FdetectedNoError.Text = " ";
            //_FnumberOfTransmission.Text = " ";
        }

        public ResultsWindow()
        {
            InitializeComponent();
        }

        public void SetResultsboxes(InfiniteNumber[] P_results, InfiniteNumber[] F_results)
        {
            ClearResults();
            _PnoError.Dispatcher.Invoke(() => { _PnoError.Text = Convert.ToString(P_results[(int)Data.noError]); });
            _PDetected.Dispatcher.Invoke(() => { _PDetected.Text = Convert.ToString(P_results[(int)Data.Detected]); });
            _PunDetected.Dispatcher.Invoke(() => { _PunDetected.Text = Convert.ToString(P_results[(int)Data.unDetected]); });
            _PdetectedNoError.Dispatcher.Invoke(() => { _PdetectedNoError.Text = Convert.ToString(P_results[(int)Data.detectedNoError]); });
            _PnumberOfTransmission.Dispatcher.Invoke(() => { _PnumberOfTransmission.Text = Convert.ToString(P_results[(int)Data.number_of_transmission]); });

            _FnoError.Dispatcher.Invoke(() => { _FnoError.Text = Convert.ToString(F_results[(int)Data.noError]); });
            _FDetected.Dispatcher.Invoke(() => { _FDetected.Text = Convert.ToString(F_results[(int)Data.Detected]); });
            _FunDetected.Dispatcher.Invoke(() => { _FunDetected.Text = Convert.ToString(F_results[(int)Data.unDetected]); });
            _FdetectedNoError.Dispatcher.Invoke(() => { _FdetectedNoError.Text = Convert.ToString(F_results[(int)Data.detectedNoError]); });
            _FnumberOfTransmission.Dispatcher.Invoke(() => { _FnumberOfTransmission.Text = Convert.ToString(F_results[(int)Data.number_of_transmission]); });
            //_PnoError.Text = Convert.ToString(P_results[(int)Data.noError]);
            //_PDetected.Text = Convert.ToString(P_results[(int)Data.Detected]);
            //_PunDetected.Text = Convert.ToString(P_results[(int)Data.unDetected]);
            //_PdetectedNoError.Text = Convert.ToString(P_results[(int)Data.detectedNoError]);
            //_PnumberOfTransmission.Text = Convert.ToString(P_results[(int)Data.number_of_transmission]);

            //_FnoError.Text = Convert.ToString(F_results[(int)Data.noError]);
            //_FDetected.Text = Convert.ToString(F_results[(int)Data.Detected]);
            //_FunDetected.Text = Convert.ToString(F_results[(int)Data.unDetected]);
            //_FdetectedNoError.Text = Convert.ToString(F_results[(int)Data.detectedNoError]);
            //_FnumberOfTransmission.Text = Convert.ToString(F_results[(int)Data.number_of_transmission]);
        }


    }
}

//Console.Clear();
//            Console.WriteLine("------------------------------WYNIKI-------------------------------------");
//            Console.WriteLine("------------------------------Pakiet-------------------------------------");
//            Console.WriteLine("|Liczba pakietow bez bledu :                        " + P_results[(int)Data.noError]);
//            Console.WriteLine("|Liczba wykrytych blednych pakietow :               " + P_results[(int)Data.Detected]);
//            Console.WriteLine("|Liczba niewykrytych blednych pakietow :            " + P_results[(int)Data.unDetected]);
//            Console.WriteLine("|Liczba wykrytych bledow, przy poprawnych danych:   " + P_results[(int)Data.detectedNoError]);
//            Console.WriteLine("|Liczba transmisji -                                " + P_results[(int)Data.number_of_transmission]);
//            Console.WriteLine("------------------------------Ramki--------------------------------------");
//            Console.WriteLine("|Liczba ramek bez bledu :                           " + F_results[(int)Data.noError]);
//            Console.WriteLine("|Liczba wykrytych blednych ramek :                  " + F_results[(int)Data.Detected]);
//            Console.WriteLine("|Liczba niewykrytych blednych ramek :               " + F_results[(int)Data.unDetected]);
//            Console.WriteLine("|Liczba wykrytych bledow, przy poprawnych danych:   " + F_results[(int)Data.detectedNoError]);
//            Console.WriteLine("|Liczba transmisji -                                " + F_results[(int)Data.number_of_transmission]);
//            Console.WriteLine("-------------------------------------------------------------------------");