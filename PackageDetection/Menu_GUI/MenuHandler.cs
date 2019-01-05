using Menu_GUI;
using PackageDetection.Results;
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PackageDetection.Menu_GUI
{
    public class MenuHandler
    {
        private static ResultsWindow Results = new ResultsWindow();
        private static MenuPackageSettings PSettings = new MenuPackageSettings();
        private static IMenuCollision menuCollision;
        private static ICollision collision;
        private static TransmissionType newTranssmision;

        //private static ulong numberOfPackagesToEnd;

        //public static ulong NumberOfPackagesToEnd { get => numberOfPackagesToEnd; set => numberOfPackagesToEnd = value; }
        public static ICollision Collision { get => collision; set => collision = value; }
        public static IMenuCollision MenuCollision { get => menuCollision;
            set
            {
                menuCollision = value;
                if(value.GetType() == typeof(MenuBitsCollision))
                    PSettings._TextInterferenceLVL.Text = "Liczba bitów do przekłamania";
                else
                    PSettings._TextInterferenceLVL.Text = "Poziom zakłóceń";
            }
        }
        public static TransmissionType NewTranssmision { get => newTranssmision; set => newTranssmision = value; }

        public static void InitMenuHandler(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            SetResultsPage(ref resultWindow);
            SetPackageSettingsPage(ref pSettings);
            //NumberOfPackagesToEnd = 0;
        }

        private static void InitTransmission()
        {
            try
            {
                collision = menuCollision.CreateCollision();
                NewTranssmision = PSettings.CreateTransmission();
                NewTranssmision.Collision_type = collision;
                PSettings.EnabledButtons(false);
                menuCollision.EnabledButtons(false);
            }
            catch
            {
                MessageBox.Show("Wprowadz dane");
            }
            
            
        }

        public static bool StartTransmission()
        {
            try
            {
                InitTransmission();
                Start_transsmision(Results);
            }
            catch
            {
                MessageBox.Show("Nie można uruchomić symulacji");
                return false;
            }
            return true;
            
        }

        public static bool StartTransmission(string fileName, bool setConfigurationByFile = true)
        {
            try
            {
                InitTransmission();
                //numberOfPackagesToEnd = 1000;
                //Console.WriteLine("numberOfPackagesToEnd : " + numberOfPackagesToEnd);
                NewTranssmision.FileName = fileName;
                Start_transsmision(Results, setConfigurationByFile);
            }
            catch
            {
                MessageBox.Show("Nie można uruchomić symulacji");
                return false;
            }
            return true;
        }


        #region SetPages
        public static void SetResultsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = Results;
        }

        public static void SetPackageSettingsPage(ref System.Windows.Controls.Frame pa)
        {
            pa.Content = PSettings;
        }
        #endregion

        public static ResultsWindow GetResultsWindow()
        {
            return Results;
        }

        public static MenuPackageSettings GetMenuPackageSettings()
        {
            return PSettings;
        }

        #region Stop/exit
        //public static void SClose()
        //{
        //    Stop();
        //    PSettings.EnabledButtons(true);
        //    menuCollision.EnabledButtons(true);
        //}
        public static void StopTransmission()
        {
            Stop();
            if (PSettings != null)
            {
                PSettings.EnabledButtons(true);
                if (menuCollision != null)
                    menuCollision.EnabledButtons(true);
            }
            
        }
        #endregion

        public static void Stop()
        {
            if (NewTranssmision != null)
                NewTranssmision.Active = false;
        }

        public static void Start_transsmision(ResultsWindow Results, bool setConfigurationByFile = true)
        {

            try
            {
                NewTranssmision.SetResultsPage(ref Results); //tu moze byc blad
                if (NewTranssmision.Active == false) //zabezpiecznie przed wielokrotnym nacisnieciem start
                {
                    NewTranssmision.Active = true;
                    NewTranssmision.UserStop(setConfigurationByFile);
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadz dane");
            }
        }

    }
}
