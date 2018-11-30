using PackageDetection.Menu_GUI;
using PackageDetection.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Menu_GUI
{
    public interface IMenuCollision
    {
        

        void SClose();

        //void SetResultsPage(ref System.Windows.Controls.Frame Results_frame);

        //void SetPackageSettingsPage(ref System.Windows.Controls.Frame menu_package);

        void SetComponentByName(string componentName, string value);

        void SetComponentsByDictionary(Dictionary<string, int> d);

        void StartTransmission();

        MenuHandler GetMenuHandler();


    }

    //if(sine_Collision != null ) sine_Collision.SClose(); // zabepieczenie przed dzialaniem niechcianych watkow w tle
    //        if (random_Collision != null)  random_Collision.SClose();

    //        bits_Collision = new MenuBitsCollision();
    //menu_collision.Content = bits_Collision;
    //        bits_Collision.SetResultsPage(ref Results_frame);
    //        bits_Collision.SetPackageSettingsPage(ref menu_package);
}
