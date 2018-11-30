using Menu_GUI;
using PackageDetection.Results;
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetection.Menu_GUI
{
    public class MenuHandler
    {

        private ResultsWindow Results = new ResultsWindow();
        private MenuPackageSettings PSettings = new MenuPackageSettings();

        private ulong numberOfPackagesToEnd;

        public ulong NumberOfPackagesToEnd { get => numberOfPackagesToEnd; set => numberOfPackagesToEnd = value; }

        public MenuHandler(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            this.SetResultsPage(ref resultWindow);
            this.SetPackageSettingsPage(ref pSettings);
            this.NumberOfPackagesToEnd = 0;
        }

        public void StartTranssmision(ICollision collision)
        {
            //menuHandler.GetMenuPackageSettings().Start_transsmision(BC, menuHandler.GetResultsWindow(), menuHandler.NumberOfPackagesToEnd);
            PSettings.Start_transsmision(collision, Results, numberOfPackagesToEnd);
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
            PSettings.Stop();
        }
        public void StopTransmission()
        {
            PSettings.Stop();
        }
        #endregion
    }
}
