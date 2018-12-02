﻿using System;
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
        //private MenuBitsCollision bits_Collision;
        //private MenuSineCollision sine_Collision;
        //private MenuRandomCollision random_Collision;

        MenuHandler menuHandler;
        //private static IMenuCollision menuCollision;
        public static TransmissionByFile transmissionByFile;
        public static System.Windows.Controls.Frame _menu_collision;

        
        public MainWindow()
        {
            InitializeComponent();
            MainWindow._menu_collision = menu_collision;
            menuHandler = new MenuHandler(ref Results_frame, ref menu_package);
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
            //transmissionByFile.SClose();
            //transmissionByFile = null;

            if (menuHandler.MenuCollision != null)
                menuHandler.SClose();

            menuHandler.MenuCollision = Helpers.MenuCollisionFactory(menuCollisionType);

            MainWindow._menu_collision.Content = menuHandler.MenuCollision;
            //menuCollision.SetResultsPage(ref Results_frame);
            //menuCollision.SetPackageSettingsPage(ref menu_package);


        }

        //przycisk Wyjdz
        private void Click_MenuExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            menuHandler.StartTransmission();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            menuHandler.StopTransmission();
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
            if (menuHandler.MenuCollision != null)
                menuHandler.SClose();

            transmissionByFile = new TransmissionByFile(@"C:\Users\lipin\source\repos\PackageDetection\PackageDetection\XMLFiles\transmissions.xml", ref menuHandler);
            CreateTransmissions(sender, e);
        }

        [STAThread]
        public static void CreateTransmissions(object sender, RoutedEventArgs e)
        {
            if (transmissionByFile.NextTransmission())
            {
                MainWindow._menu_collision.Content = transmissionByFile.GetMenuHandler().MenuCollision;
                transmissionByFile.StartTransmission();
            }
            else
            {
                //Console.WriteLine("Skonczylem");
            }
                
        }
    }

}