﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PackageDetection.ConfigurationModule.TransmissionDataClass;
using PackageDetection.Menu_GUI;
using PackageDetection.MessageBuilderPackage;
using PackageDetection.Results;
using Projekt_Kolko;

namespace Menu_GUI
{

    public partial class MenuSineCollision : Page, IMenuCollision
    {

        //private MenuHandler menuHandler;

        public MenuSineCollision()
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

        //public void StartTransmission()
        //{
        //    try
        //    {
        //        menuHandler.Collision = CreateCollision();
        //        menuHandler.StartTranssmision();
        //        //menuHandler.GetMenuPackageSettings().Start_transsmision(SC, menuHandler.GetResultsWindow(), numberOfPackagesToEnd);
        //    }
        //    catch (FormatException)
        //    {
        //        MessageBox.Show("Wprowadz dane");
        //    }
        //}

        public ICollision CreateCollision()
        {
            double x_start = Convert.ToDouble(_XStart.Text);
            double x_end = Convert.ToDouble(_XEnd.Text);

            if (x_start > x_end)
            {
                void Swap<T>(ref T x, ref T y) { T t = y; y = x; x = t; }
                Swap(ref x_start, ref x_end);
            }

            return new SineCollision(1, 2, 0, x_start, x_end);
        }


        //Ochrona przed wpisywaniem niepoparwnych danych
        #region DataInBox And Checkbox 
        private void DataInBox_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 10000, 0, 5);
        }
        #endregion


        public void SetComponentByName(string componentName, string value)
        {
            throw new NotImplementedException();
        }


        public void SetComponentsByDictionary(Dictionary<string, int> d)
        {
            MessageBuilder.AddTitleMessage("SINE COLLISION");
            _XStart.Text =  d[SineCollisionData.X_Start].ToString();
            MessageBuilder.AddInfoMessage("Set x axis start: " + _XStart.Text);
            _XEnd.Text =    d[SineCollisionData.X_End].ToString();
            MessageBuilder.AddInfoMessage("Set x axis end: " + _XEnd.Text);
        }

        public void EnabledButtons(bool enable)
        {
            _XStart.IsEnabled = enable;
            _XEnd.IsEnabled = enable;
        }
    }
}
