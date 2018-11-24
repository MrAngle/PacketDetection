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
        ulong _number_of_transsmision;
        IControl control_type;
        ICollision collision_type;
        int interference_level = 1000;
        int size_of_frame = 10;
        int numbers_of_frame_in_package = 10;
        int size_control_part;
        ResultsStorage ResultsS = new ResultsStorage(); // przechowuje wyniki
        ResultsWindow RWindow;
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

        private bool active = false; // flaga sprawdzajaca czy Transmisja jest wlaczona
        public bool Active { get => active; set => active = value; }

        public void SetResultsPage(ref ResultsWindow r)
        {
            RWindow = r;
        }

        public TransmissionType(ulong _number_of_transsmision, IControl control_type,
            ICollision collision_type, int interference_level = 1000,
            int size_of_frame = 10, int numbers_of_frame_in_package = 10, int size_control_part = Functions.FLEXIBLE)
        {
            this._number_of_transsmision = _number_of_transsmision;
            this.control_type = control_type;
            this.collision_type = collision_type;
            this.interference_level = interference_level;
            this.size_of_frame = size_of_frame;
            this.numbers_of_frame_in_package = numbers_of_frame_in_package;
            this.size_control_part = size_control_part;
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
                collision_type.DoCollision(pak, this.interference_level);
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

        

        public void UserStop()
        {

            BackgroundWorker worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            worker.DoWork += Stop;


            worker.RunWorkerAsync(1);

        }

        public void Stop(object sender, DoWorkEventArgs e)
        {
            while(Active != false)
                this.Normal();

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
