using Menu_GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PackageDetection.ConfigurationModule
{
    class TransmissionByFile : MenuCollision
    {
        MenuCollision menuCollision;




        public void ConfigSetComponentByName(string componentName, string value)
        {
            menuCollision.ConfigSetComponentByName(componentName, value);
        }

        public void SClose()
        {
            menuCollision.SClose();
        }

        public void SetPackageSettingsPage(ref Frame menu_package)
        {
            menuCollision.SetPackageSettingsPage(ref menu_package);
        }

        public void SetResultsPage(ref Frame Results_frame)
        {
            menuCollision.SetResultsPage(ref Results_frame);
        }
    }
}
//moja klasa, to jest dobra baza
//pieniadze sa prywatne, a stringi łososiokształtne