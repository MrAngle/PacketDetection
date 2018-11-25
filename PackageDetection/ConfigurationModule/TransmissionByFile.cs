using Menu_GUI;
using Projekt_Kolko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PackageDetection.ConfigurationModule
{
    public class TransmissionByFile
    {
        MenuCollision menuCollision;

        public TransmissionByFile(string fileName)
        {
            //TODO dodac czytanie z pliku
        }

        public MenuCollision GetMenuCollision()
        {
            return menuCollision;
        }

        public int NextTransmission(ref System.Windows.Controls.Frame resultWindow, ref System.Windows.Controls.Frame pSettings)
        {
            menuCollision = Helpers.MenuCollisionFactory(Helpers.BIT_COLLISION, ref resultWindow, ref pSettings);
            menuCollision.SetComponentByName("_firstindex", "2");
            SetPackageSettings();
            return 1;
        }

        private void SetPackageSettings()
        {
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsControlPart("2");
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetBitsInFrame("43");
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetFramesInPackage("342");
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetInterferenceLVL("234");
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetNumberOfTransmission("23");
            menuCollision.GetMenuHandler().GetMenuPackageSettings().SetControlType(true);
        }

        public void ConfigSetComponentByName(string componentName, string value)
        {
            menuCollision.SetComponentByName(componentName, value);
        }

        public void SClose()
        {
            menuCollision.SClose();
        }

    }
}
//moja klasa, to jest dobra baza
//pieniadze sa prywatne, a stringi łososiokształtne