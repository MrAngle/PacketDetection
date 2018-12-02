using PackageDetection;
using PackageDetection.MessageBuilderPackage;
using PackageDetection.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Projekt_Kolko
{
    public class TransmissionType
    {
        private BackgroundWorker worker = new BackgroundWorker();
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        ulong _number_of_transsmision;
        IControl control_type;
        ICollision collision_type;
        int interference_level = 1000;
        int size_of_frame = 10;
        int numbers_of_frame_in_package = 10;
        int size_control_part = 4;
        bool setConfigurationByFile = false;
        string fileName = "test";

        ResultsStorage ResultsS = new ResultsStorage(); // przechowuje wyniki
        ResultsWindow RWindow;

        private bool active = false; // flaga sprawdzajaca czy Transmisja jest wlaczona
        public bool Active { get => active; set => active = value; }
        public BackgroundWorker Worker { get => worker; set => worker = value; }
        public AutoResetEvent ResetEvent { get => _resetEvent; set => _resetEvent = value; }
        public string FileName { get => fileName; set { fileName = value; setConfigurationByFile = true; }  }
        public ICollision Collision_type { get => collision_type; set => collision_type = value; }

        public const int DATALENGHT = 5;

        public enum Data
        {
            noError = 0,
            Detected = 1,
            unDetected = 2,
            detectedNoError = 3,
            number_of_transmission = 4
        }
        // 0 - noError
        // 1 - Detected
        // 2 - unDetected
        // 3 - detectedNoError
        // 4 - number_of_transmission
        

        ulong[] frame_results = new ulong[5] { 0, 0, 0, 0, 0 };
        ulong[] package_results = new ulong[5] { 0, 0, 0, 0, 0 };

        

        public void SetResultsPage(ref ResultsWindow r)
        {
            RWindow = r;
        }


        public TransmissionType(ulong _number_of_transsmision, IControl control_type,
            int interference_level = 1000,
            int size_of_frame = 10, int numbers_of_frame_in_package = 10, int size_control_part = Helpers.FLEXIBLE,bool setConfigurationByFile = false)
        {
            this._number_of_transsmision = _number_of_transsmision;
            this.control_type = control_type;
            this.interference_level = interference_level;
            this.size_of_frame = size_of_frame;
            this.numbers_of_frame_in_package = numbers_of_frame_in_package;
            this.size_control_part = size_control_part;
            this.setConfigurationByFile = setConfigurationByFile;
        }



        public void Show()
        {
            ResultsS.ShowResults(ref RWindow);
        }

        public void Normal()
        {
            ClearResults();
            for (ulong i = 0; i < _number_of_transsmision; i++)
            {
                Package pak = new Package();
                pak.GenerateFrameList(numbers_of_frame_in_package, size_of_frame, control_type, size_control_part);
                Collision_type.DoCollision(pak, this.interference_level);
                ResultsSelectorPackage(pak.CheckPackage());
                foreach (var item in pak.GetFrames())
                {
                    ResultsSelectorFrame(item.CheckFrame());
                }
                pak.DeleteFrames();
                pak = null;
                //GC.Collect();
            }
            this.package_results[(int)Data.number_of_transmission] = _number_of_transsmision;
            this.frame_results[(int)Data.number_of_transmission] = _number_of_transsmision * (ulong)numbers_of_frame_in_package;
            ResultsS.AddResults(package_results, frame_results);
            ResultsS.ShowResults(ref RWindow);
            //Task.Delay(100000);



        }


        [STAThread]
        public void UserStop(ulong numberOfPackagesToEnd = 0)
        {
            //Console.WriteLine("jestem tutaj");
            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            if (numberOfPackagesToEnd <= 0)
                worker.DoWork += DoWorkAllTime;
            else
                worker.DoWork += DoWorkPackagesLimit;

            if(setConfigurationByFile)
                worker.RunWorkerCompleted += (obj, e) => FinishExecution();

            worker.RunWorkerAsync(numberOfPackagesToEnd);
            
        }

        private void FinishExecution()
        {
            MessageBuilder.AddInfoMessage("END");
            Console.WriteLine(MessageBuilder.GetMessage());
            MessageBuilder.WriteMessageToFile(fileName);
            MessageBuilder.ClearMessage();
            //System.Threading.Thread.Sleep(5000);
            MainWindow.CreateTransmissions(new object(), new System.Windows.RoutedEventArgs());
        }

        public void DoWorkAllTime(object sender, DoWorkEventArgs e)
        {
            while(Active != false)
                this.Normal();

            //_resetEvent.Set();
        }

        [STAThread]
        public void DoWorkPackagesLimit(object sender, DoWorkEventArgs e)
        {
            ulong numberOfPackagesToEnd = (ulong)e.Argument;

            MessageBuilder.AddMainTitleMessage("START TRANSMISSIONS");
            while (numberOfPackagesToEnd >= ResultsS.P_results[(int)Data.number_of_transmission] && Active != false)
                this.Normal();

            AddResultInfoToMessageBuilder();
        }

        private void AddResultInfoToMessageBuilder()
        {
            MessageBuilder.AddTitleMessage("RESULTS");
            MessageBuilder.AddTitleMessage("Packages");
            MessageBuilder.AddInfoMessage("No errors in the package: " + ResultsS.P_results[(int)Data.noError]);
            MessageBuilder.AddInfoMessage("Detected errors: "+ ResultsS.P_results[(int)Data.Detected]);
            MessageBuilder.AddInfoMessage("Undetected errors: " + ResultsS.P_results[(int)Data.unDetected]);
            MessageBuilder.AddInfoMessage("Detected error in clear package : " + this.package_results[(int)Data.detectedNoError]);
            MessageBuilder.AddInfoMessage("Number of packages sent: " + ResultsS.P_results[(int)Data.number_of_transmission]);

            MessageBuilder.AddTitleMessage("Frames");
            MessageBuilder.AddInfoMessage("No errors in the frame: " + this.ResultsS.F_results[(int)Data.noError]);
            MessageBuilder.AddInfoMessage("Detected error: " + this.ResultsS.F_results[(int)Data.Detected]);
            MessageBuilder.AddInfoMessage("Undetected errors: " + this.ResultsS.F_results[(int)Data.unDetected]);
            MessageBuilder.AddInfoMessage("Detected error in clear frame : " + this.ResultsS.F_results[(int)Data.detectedNoError]);
            MessageBuilder.AddInfoMessage("Number of packages sent: " + this.ResultsS.F_results[(int)Data.number_of_transmission]);
        }


        private void ClearResults()
        {
            for (int i = 0; i < 5; i++)
            {
                this.package_results[i] = 0;
                this.frame_results[i] = 0;
            }
        }
        private void ResultsSelectorPackage(byte result)
        {
            ResultsSelector(result, package_results);
        }
        private void ResultsSelectorFrame(byte result)
        {
            ResultsSelector(result, frame_results);
        }
        private void ResultsSelector(byte result, ulong[] results)
        {
            switch (result)
            {
                case 0:
                    results[(int)Data.noError]++;
                    break;
                case 1:
                    results[(int)Data.Detected]++;
                    break;
                case 2:
                    results[(int)Data.unDetected]++;
                    break;
                case 3:
                    results[(int)Data.detectedNoError]++;
                    break;
                default:
                    Console.WriteLine("PODANO ZLY WYNIK  ");
                    break;
            }
        }
        



    }

}
