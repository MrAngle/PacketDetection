using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using PackageDetection.Menu_GUI;
using PackageDetection.Results;
using Projekt_Kolko;

namespace Menu_GUI
{

    public partial class MenuSineCollision : Page, IMenuCollision
    {

        private MenuHandler menuHandler;

        private SineCollision SC;
        public MenuSineCollision(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
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
                if (SC == null)
                {
                    SC = CreateBitsCollision(_XStart.Text, _XEnd.Text);
                }
                menuHandler.StartTranssmision(SC);
                //menuHandler.GetMenuPackageSettings().Start_transsmision(SC, menuHandler.GetResultsWindow(), numberOfPackagesToEnd);
            }
            catch (FormatException)
            {
                SC = null;
                MessageBox.Show("Wprowadz dane");
            }
        }

        protected SineCollision CreateBitsCollision(string start, string end)
        {
            double x_start = Convert.ToDouble(start);
            double x_end = Convert.ToDouble(end);

            

            if (x_start > x_end)
            {
                void Swap<T>(ref T x, ref T y) { T t = y; y = x;  x = t; }
                Swap(ref x_start,ref x_end);
            }


            return  new SineCollision(1, 2, 0, x_start, x_end);
        }

        //Ochrona przed wpisywaniem niepoparwnych danych
        #region DataInBox And Checkbox 
        private void DataInBox_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 10000, 0, 5);
        }
        #endregion


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
            _XStart.Text =  d[SineCollisionData.X_Start].ToString();
            _XEnd.Text =    d[SineCollisionData.X_End].ToString();
        }
    }
}
