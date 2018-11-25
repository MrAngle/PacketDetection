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
using PackageDetection.Results;
using Projekt_Kolko;

namespace Menu_GUI
{

    public partial class MenuRandomCollision : Page, MenuCollision
    {

        private ResultsWindow Results = new ResultsWindow();
        private MenuPackageSettings PSettings = new MenuPackageSettings();
        private RandomCollision RC;
        public MenuRandomCollision()
        {
            InitializeComponent();
        }

        #region SetPages
        public void SetResultsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = Results;
        }

        public void SetPackageSettingsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = PSettings;
        }
        #endregion

        #region Stop/exit
        public void SClose()
        {
            PSettings.Stop();
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            PSettings.Stop();
        }
        #endregion


        
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RC == null)
                {
                    RC = new RandomCollision();
                }
                PSettings.Start_transsmision(RC, Results);
            }
            catch (FormatException)
            {
                RC = null;
                MessageBox.Show("Wprowadz dane");
            }
        }

        public void ConfigSetComponentByName(string componentName, string value)
        {

        }

    }
}
