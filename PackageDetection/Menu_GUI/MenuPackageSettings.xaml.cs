using PackageDetection.Results;
using Projekt_Kolko;
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

namespace Menu_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MenuSet.xaml
    /// </summary>
    public partial class MenuPackageSettings : Page
    {
        //private TransmissionType newTranssmision;

        //public TransmissionType NewTranssmision { get => newTranssmision; set => newTranssmision = value; }

        public MenuPackageSettings()
        {
            InitializeComponent();
        }

        public TransmissionType CreateTransmission()
        {
            int toInt(string str) // zamiana na Int
            { return Convert.ToInt32(str); }

            ulong numOfT = Convert.ToUInt64(_NumberOfTransmission.Text);
            IControl contType;
            if (_CRC.IsChecked == true)
                contType = new CRCControl();
            else if (_CheckSum.IsChecked == true)
                contType = new CheckSumControl();
            else 
                contType = new ParityBitControl();


            int intLvl = toInt(_InterferenceLVL.Text);
            int sizeOfFra = toInt(_BitsInFrame.Text) * 8;
            int numFraInPac = toInt(_FramesInPackage.Text);
            int sizeOfControl = toInt(_BitsControlPart.Text) * 8;
            ulong numberOfPackageToEnd = Convert.ToUInt64(_PackageToEnd.Text);

            Console.WriteLine("sizeOfFra : " + sizeOfFra);
            Console.WriteLine("sizeOfControl : " + sizeOfControl);

            return new TransmissionType(numOfT, contType, intLvl, sizeOfFra, numFraInPac, numberOfPackageToEnd, sizeOfControl);
        }

        //Ochrona przed wpisywaniem niepoparwnych danych
        #region DataInBox And Checkbox
        private void DataInBox_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 10000, 0, 5);
        }

        private void DataInBoxInterference_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 1000, 0, 5);
        }

        private void DataInBoxPackagesToEnd_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 9999999999, 0, 10);
        }

        // wybor konkretnej grupy bitow do zamiany | wylaczenie losowego wyboru bitow

        // Prosty sposob na uniemozliwienie zaznaczenia wiecej niz 1 checkbox 
        private void ClickCheckCRC_(object sender, RoutedEventArgs e)
        {
            SetCRC_();
        }
        private void ClickCheckCheckSum_(object sender, RoutedEventArgs e)
        {
            SetCheckSum_();
        }
        private void ClickCheckParityBit_(object sender, RoutedEventArgs e)
        {
            SetParityBit_();
        }

        private void SetCRC_()
        {
            _CRC.SetCurrentValue(CheckBox.IsCheckedProperty, true);

            _CheckSum.SetCurrentValue(CheckBox.IsCheckedProperty, false);
            _ParityBit.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }
        private void SetCheckSum_()
        {
            _CheckSum.SetCurrentValue(CheckBox.IsCheckedProperty, true);

            _ParityBit.SetCurrentValue(CheckBox.IsCheckedProperty, false);
            _CRC.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }

        private void SetParityBit_()
        {
            _ParityBit.SetCurrentValue(CheckBox.IsCheckedProperty, true);

            _CheckSum.SetCurrentValue(CheckBox.IsCheckedProperty, false);
            _CRC.SetCurrentValue(CheckBox.IsCheckedProperty, false);
        }

        #endregion


        #region SetValues
        //int toInt(string str) // zamiana na Int
        //{ return Convert.ToInt32(str); }

        //ulong numOfT = Convert.ToUInt64(_NumberOfTransmission.Text);
        //IControl contType = new ParityBitControl(); // zabezpiecznie przed niezaznaczniem zadnego checkboxu
        //    if (_CRC.IsChecked == true)
        //        contType = new CRCControl();
        //    else if (_CheckSum.IsChecked == true)
        //        contType = new CheckSumControl();
        //    else if (_ParityBit.IsChecked == true)
        //        contType = new ParityBitControl();

        //int intLvl = toInt(_InterferenceLVL.Text);
        //int sizeOfFra = toInt(_BitsInFrame.Text);
        //int numFraInPac = toInt(_FramesInPackage.Text);
        //int sizeOfControl = toInt(_BitsControlPart.Text);
        public void SetNumberOfTransmission(ulong str)
        {
            _NumberOfTransmission.Text = str.ToString();
        }

        public void SetInterferenceLVL(int str)
        {
            _InterferenceLVL.Text = str.ToString();
        }

        public void SetBitsInFrame(int str)
        {
            _BitsInFrame.Text = str.ToString();
        }

        public void SetFramesInPackage(int str)
        {
            _FramesInPackage.Text = str.ToString();
        }

        public void SetBitsControlPart(int str)
        {
            _BitsControlPart.Text = str.ToString();
        }

        public void SetPackageToEnd(ulong str)
        {
            _PackageToEnd.Text = str.ToString();
        }

        public void SetControlType(string controlName)
        {
            controlName = controlName.ToLower();
            switch (controlName)
            {
                case (CheckSumControl.NAME):
                    SetCheckSum_();
                    break;
                case (ParityBitControl.NAME):
                    SetParityBit_();
                    break;
                case (CRCControl.NAME):
                    SetCRC_();
                    break;
            }
            //Console.WriteLine("***** Something wrong in - MenuPackageSettings.SetControlType : ");
        }


        #endregion

        public void EnabledButtons(bool enable)
        {
            _BitsControlPart.IsEnabled = enable;
            _BitsInFrame.IsEnabled = enable;
            _CheckSum.IsEnabled = enable;
            _CRC.IsEnabled = enable;
            _FramesInPackage.IsEnabled = enable;
            _InterferenceLVL.IsEnabled = enable;
            _NumberOfTransmission.IsEnabled = enable;
            _ParityBit.IsEnabled = enable;
            _PackageToEnd.IsEnabled = enable;

        }
    }
}
