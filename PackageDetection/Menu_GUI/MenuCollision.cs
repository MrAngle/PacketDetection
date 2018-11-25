using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_GUI
{
    interface MenuCollision
    {
        void SClose();

        void SetResultsPage(ref System.Windows.Controls.Frame Results_frame);

        void SetPackageSettingsPage(ref System.Windows.Controls.Frame menu_package);
    }

    //if(sine_Collision != null ) sine_Collision.SClose(); // zabepieczenie przed dzialaniem niechcianych watkow w tle
    //        if (random_Collision != null)  random_Collision.SClose();

    //        bits_Collision = new MenuBitsCollision();
    //menu_collision.Content = bits_Collision;
    //        bits_Collision.SetResultsPage(ref Results_frame);
    //        bits_Collision.SetPackageSettingsPage(ref menu_package);
}
