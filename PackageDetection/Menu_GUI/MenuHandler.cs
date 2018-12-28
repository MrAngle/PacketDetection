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

        private static ulong numberOfPackagesToEnd;

        public static ulong NumberOfPackagesToEnd { get => numberOfPackagesToEnd; set => numberOfPackagesToEnd = value; }
        public static ICollision Collision { get => collision; set => collision = value; }
        public static IMenuCollision MenuCollision { get => menuCollision; set => menuCollision = value; }
        public static TransmissionType NewTranssmision { get => newTranssmision; set => newTranssmision = value; }

        public static void InitMenuHandler(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            SetResultsPage(ref resultWindow);
            SetPackageSettingsPage(ref pSettings);
            NumberOfPackagesToEnd = 0;
        }

        private static void InitTransmission()
        {
            //PSettings.NewTranssmision = new TransmissionType();
            
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

            //menuHandler.GetMenuPackageSettings().Start_transsmision(BC, menuHandler.GetResultsWindow(), menuHandler.NumberOfPackagesToEnd);
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

        public static void StartTransmission(string fileName)
        {
            InitTransmission();
            NewTranssmision.FileName = fileName;
            Start_transsmision(Results, numberOfPackagesToEnd);
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

        public static void Start_transsmision(ResultsWindow Results, ulong numberOfPackagesToEnd = 0)
        {

            try
            {
                NewTranssmision.SetResultsPage(ref Results); //tu moze byc blad
                if (NewTranssmision.Active == false) //zabezpiecznie przed wielokrotnym nacisnieciem start
                {
                    NewTranssmision.Active = true;
                    NewTranssmision.UserStop(numberOfPackagesToEnd);
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadz dane");
            }
        }

    }
}
