using Menu_GUI;
using PackageDetection.Results;
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

        public MenuHandler(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            this.SetResultsPage(ref resultWindow);
            this.SetPackageSettingsPage(ref pSettings);
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
