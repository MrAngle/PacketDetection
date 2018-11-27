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
        private TransmissionType newTranssmision;

        public TransmissionType NewTranssmision { get => newTranssmision; set => newTranssmision = value; }

        public MenuPackageSettings()
        {
            InitializeComponent();
        }



        public void Stop()
        {
            if (NewTranssmision != null)
                NewTranssmision.Active = false;
        }
        public void SClose()
        {
            Stop();
        }
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            Stop();
        }

      

        public TransmissionType CreateTransmission(ICollision Collision)
        {


            int toInt(string str) // zamiana na Int
            { return Convert.ToInt32(str); }

            ulong numOfT = Convert.ToUInt64(_NumberOfTransmission.Text);
            IControl contType;
            if (_CRC.IsChecked == true)
                contType = new CRCControl();
            else if (_CheckSum.IsChecked == true)
                contType = new CheckSumControl();
            else if (_ParityBit.IsChecked == true)
                contType = new ParityBitControl(); 
            else
                contType = new ParityBitControl(); // zabezpiecznie przed niezaznaczniem zadnego checkboxu TODO: zeby obslugiwalo to jakos sensownie

            int intLvl = toInt(_InterferenceLVL.Text);
            int sizeOfFra = toInt(_BitsInFrame.Text);
            int numFraInPac = toInt(_FramesInPackage.Text);
            int sizeOfControl = toInt(_BitsControlPart.Text);

            return new TransmissionType(numOfT, contType, Collision, intLvl, sizeOfFra, numFraInPac, sizeOfControl);
        }

        public void Start_transsmision(ICollision Collision, ResultsWindow Results)
        {

            try
            {
                if (newTranssmision == null)
                {
                    newTranssmision = CreateTransmission(Collision);
                    newTranssmision.SetResultsPage(ref Results); //tu moze byc blad
                }
                if (newTranssmision.Active == false) //zabezpiecznie przed wielokrotnym nacisnieciem start
                {
                    newTranssmision.Active = true;
                    newTranssmision.UserStop();
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadz dane");
            }
        }

        //Ochrona przed wpisywaniem niepoparwnych danych
        #region DataInBox And Checkbox
        private void DataInBox_(object sender, TextChangedEventArgs e)
        {
            TextBox n = (TextBox)sender;
            n.Text = Data_verification.Check(n.Text, 10000, 0, 5);
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
    }
}
