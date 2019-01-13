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
using PackageDetection.ConfigurationModule;
using PackageDetection.Menu_GUI;
using PackageDetection.MessageBuilderPackage;
using PackageDetection.Results;
using Projekt_Kolko;

namespace PackageDetection
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>

    

    public partial class MainWindow : Window
    {
        //Spytać o poprawność programu
        //Ustalenie detekcji z poziomu ramki
        //Wprowadzenie zmian z suma bajtów
        //Zmiana z rozmiarem ramk i części kontrolnej
        //Możliwe określenie liczby przetworzonych pakietów z poziomu GUI
        //Czy usunąc "wykrycie błędu dla poprawnych danych"
        //Kopiowanie tekstu użytego przeze mnie
        //Liczba zdjęć
        //Puszczenie badania
        //Wysłanie dokumentacji - czy na mail
        //Źródło screenów programów

        //MenuHandler menuHandler;
        //private static IMenuCollision menuCollision;
        public static TransmissionByFile transmissionByFile;
        public static System.Windows.Controls.Frame _menu_collision;

        
        public MainWindow()
        {
            InitializeComponent();
            MainWindow._menu_collision = menu_collision;
            MenuHandler.InitMenuHandler(ref Results_frame, ref menu_package);
        }

        private void Click_bitsCollision(object sender, RoutedEventArgs e)
        {
            InitializeNewCollisionPage(Helpers.BIT_COLLISION);
        }

        private void Click_sinusCollision(object sender, RoutedEventArgs e)
        {
            InitializeNewCollisionPage(Helpers.SINE_COLLISION);
        }

        private void Click_randomCollision(object sender, RoutedEventArgs e)
        {
            InitializeNewCollisionPage(Helpers.RANDOM_COLLISION);
        }


        public void InitializeNewCollisionPage(int menuCollisionType)
        {
            _read_from_file_button.IsEnabled = true;
            _start_button.IsEnabled = true;

            MenuHandler.StopTransmission();

            MenuHandler.MenuCollision = Helpers.MenuCollisionFactory(menuCollisionType);

            MainWindow._menu_collision.Content = MenuHandler.MenuCollision;
        }

        //przycisk Wyjdz
        private void Click_MenuExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            
            if(MenuHandler.StartTransmission("Normal", false))
            {
                _read_from_file_button.IsEnabled = false;
                _start_button.IsEnabled = false;
            }
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            
            MenuHandler.StopTransmission();
            _start_button.IsEnabled = true;
            _read_from_file_button.IsEnabled = true;
        }

        //Override the onClose method in the Application Main window
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zakonczyc?", "", MessageBoxButton.OKCancel);
            
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


        [STAThread]
        private void Click_transmissionByFile(object sender, RoutedEventArgs e)
        {
            MenuHandler.StopTransmission();
            _start_button.IsEnabled = false;
            _read_from_file_button.IsEnabled = false;
            //Console.WriteLine();

            transmissionByFile = new TransmissionByFile(@"C:\Users\lipin\source\repos\PackageDetection\PackageDetection\XMLFiles\transmissions.xml");
            if (MenuHandler.NewTranssmision != null )
                MenuHandler.NewTranssmision.Active = true;
            CreateTransmissions(sender, e);   
        }

        [STAThread]
        public static void CreateTransmissions(object sender, RoutedEventArgs e)
        {
            if(MenuHandler.NewTranssmision == null || MenuHandler.NewTranssmision.Active == true)
            { 
                if (transmissionByFile.NextTransmission())
                {
                    MainWindow._menu_collision.Content = MenuHandler.MenuCollision;
                    transmissionByFile.StartTransmission();
                }
                else
                {
                    MenuHandler.StopTransmission();
                }
            }
            //_start_button.IsEnabled = true;
        }
    }

}