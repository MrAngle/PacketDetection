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

        private ResultsWindow Results = new ResultsWindow();
        private MenuPackageSettings PSettings = new MenuPackageSettings();
        private IMenuCollision menuCollision;
        private ICollision collision;
        private TransmissionType newTranssmision;

        private ulong numberOfPackagesToEnd;

        public ulong NumberOfPackagesToEnd { get => numberOfPackagesToEnd; set => numberOfPackagesToEnd = value; }
        public ICollision Collision { get => collision; set => collision = value; }
        public IMenuCollision MenuCollision { get => menuCollision; set => menuCollision = value; }

        public MenuHandler(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            this.SetResultsPage(ref resultWindow);
            this.SetPackageSettingsPage(ref pSettings);
            this.NumberOfPackagesToEnd = 0;
        }

        private void InitTransmission()
        {
            //PSettings.NewTranssmision = new TransmissionType();
            collision = menuCollision.CreateCollision();
            newTranssmision = PSettings.CreateTransmission();
            newTranssmision.Collision_type = collision;
            PSettings.EnabledButtons(false);
            menuCollision.EnabledButtons(false);
        }

        public void StartTransmission()
        {
            
            //menuHandler.GetMenuPackageSettings().Start_transsmision(BC, menuHandler.GetResultsWindow(), menuHandler.NumberOfPackagesToEnd);
            InitTransmission();
            Start_transsmision(Results, numberOfPackagesToEnd);
        }

        public void StartTransmission(string fileName)
        {
            InitTransmission();
            newTranssmision.FileName = fileName;
            Start_transsmision(Results, numberOfPackagesToEnd);
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

        public ResultsWindow GetResultsWindow()
        {
            return Results;
        }

        public MenuPackageSettings GetMenuPackageSettings()
        {
            return PSettings;
        }

        #region Stop/exit
        public void SClose()
        {
            Stop();
            PSettings.EnabledButtons(true);
            menuCollision.EnabledButtons(true);
        }
        public void StopTransmission()
        {
            Stop();
            PSettings.EnabledButtons(true);
            menuCollision.EnabledButtons(true);
        }
        #endregion

        public void Stop()
        {
            if (newTranssmision != null)
                newTranssmision.Active = false;
        }

        public void Start_transsmision(ResultsWindow Results, ulong numberOfPackagesToEnd = 0)
        {

            try
            {
                newTranssmision.SetResultsPage(ref Results); //tu moze byc blad
                if (newTranssmision.Active == false) //zabezpiecznie przed wielokrotnym nacisnieciem start
                {
                    newTranssmision.Active = true;
                    newTranssmision.UserStop(numberOfPackagesToEnd);
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadz dane");
            }
        }

    }
}
