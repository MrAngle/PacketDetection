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
using PackageDetection.Menu_GUI;
using PackageDetection.Results;
using Projekt_Kolko;

namespace Menu_GUI
{

    public partial class MenuRandomCollision : Page, IMenuCollision
    {
        private MenuHandler menuHandler;

        private RandomCollision RC;
        public MenuRandomCollision(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            try
            {
                InitializeComponent();
                menuHandler = new MenuHandler(ref resultWindow, ref pSettings);
            }
            catch (Exception)
            {
                MessageBox.Show("Blad przy inicjalizacji danych");
            }

        }


        #region Stop/exit
        public void SClose()
        {
            menuHandler.SClose();
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            menuHandler.StopTransmission();
        }
        #endregion



        private void Button_Start(object sender, RoutedEventArgs e)
        {
            StartTransmission();
        }

        public void StartTransmission()
        {
            try
            {
                if (RC == null)
                {
                    RC = new RandomCollision();
                }
                menuHandler.StartTranssmision(RC);
                //menuHandler.GetMenuPackageSettings().Start_transsmision(RC, menuHandler.GetResultsWindow(), numberOfPackagesToEnd);
            }
            catch (FormatException)
            {
                RC = null;
                MessageBox.Show("Wprowadz dane");
            }
        }


        public void SetComponentByName(string componentName, string value)
        {
            throw new NotImplementedException();
        }

        public MenuHandler GetMenuHandler()
        {
            return menuHandler;
        }

        public void SetComponentsByDictionary(Dictionary<string, int> d)
        {
            /*
             * Nothing to set
             */
        }
    }
}
