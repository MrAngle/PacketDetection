using System;
using System.Collections.Generic;
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
using PackageDetection.Menu_GUI;
using PackageDetection.MessageBuilderPackage;
using PackageDetection.Results;
using Projekt_Kolko;

namespace Menu_GUI
{

    public partial class MenuRandomCollision : Page, IMenuCollision
    {
        //private MenuHandler menuHandler;

        public MenuRandomCollision()
        {
            try
            {
                InitializeComponent();
                //menuHandler = new MenuHandler(ref resultWindow, ref pSettings);
            }
            catch (Exception)
            {
                MessageBox.Show("Blad przy inicjalizacji danych");
            }

        }




        public ICollision CreateCollision()
        {
            return new RandomCollision(); 
        }



        public void SetComponentByName(string componentName, string value)
        {
            throw new NotImplementedException();
        }



        public void SetComponentsByDictionary(Dictionary<string, int> d)
        {
            MessageBuilder.AddTitleMessage("RANDOM COLLISION");
            /*
             * Nothing to set
             */
        }

        public void EnabledButtons(bool enable)
        {
            /*
             * Nothing to enable
             */
        }


    }
}
