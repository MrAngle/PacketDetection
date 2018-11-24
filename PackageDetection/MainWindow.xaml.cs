using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Menu_GUI;
using PackageDetection.Results;

namespace PackageDetection
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>

    

    public partial class MainWindow : Window
    {
        private MenuBitsCollision bits_Collision;
        private MenuSineCollision sine_Collision;
        private MenuRandomCollision random_Collision;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_bitsCollision(object sender, RoutedEventArgs e)
        {
            if(sine_Collision != null ) sine_Collision.SClose(); // zabepieczenie przed dzialaniem niechcianych watkow w tle
            if (random_Collision != null)  random_Collision.SClose();

            bits_Collision = new MenuBitsCollision();
            menu_collision.Content = bits_Collision;
            bits_Collision.SetResultsPage(ref Results_frame);
            bits_Collision.SetPackageSettingsPage(ref menu_package);
        }

        private void Click_sinusCollision(object sender, RoutedEventArgs e)
        {
            if (random_Collision != null) bits_Collision.SClose(); // zabepieczenie przed dzialaniem niechcianych watkow w tle
            if (random_Collision != null) random_Collision.SClose();

            sine_Collision = new MenuSineCollision();
            menu_collision.Content = sine_Collision;
            sine_Collision.SetResultsPage(ref Results_frame);
            sine_Collision.SetPackageSettingsPage(ref menu_package);


        }

        private void Click_randomCollision(object sender, RoutedEventArgs e)
        {
            if (random_Collision != null) bits_Collision.SClose();// zabepieczenie przed dzialaniem niechcianych watkow w tle
            if (sine_Collision != null) sine_Collision.SClose();

            random_Collision = new MenuRandomCollision();
            menu_collision.Content = random_Collision;
            random_Collision.SetResultsPage(ref Results_frame);
            random_Collision.SetPackageSettingsPage(ref menu_package);


        }

        //przycisk Wyjdz
        private void Click_MenuExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        //Override the onClose method in the Application Main window
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zakonczyc?", "",
                                                  MessageBoxButton.OKCancel);
            
            if (result == MessageBoxResult.Cancel)
            {
                
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(Environment.ExitCode);
            }
            base.OnClosing(e);
        }


    }

}