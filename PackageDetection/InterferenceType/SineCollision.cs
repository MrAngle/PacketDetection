
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class SineCollision : ICollision
    {
        /*
        * Sinus w postaci a*sin(b*x + c)
        * Nalezy zdefiniowac wspolczynniki a, b i c
        */
        double arg_a = 1;
        double arg_b = 2;
        double arg_c = 0;
        double x_step = 0;
        double x_start = -3.14;
        double x_end = 3.14;
        double x = 0;
        /*
        * Domyslny wzor sin(2x) dziedzina <-3,14 - 3,14>, krok 0.1
        */
        private double Function(double x)
        {
            return arg_a * Math.Sin(arg_b * x);
        }
        public SineCollision()
        {
            x = x_start;
        }
        public SineCollision(double _arg_a, double _arg_b, double _arg_c, double _x_start = -3.14, double _x_end = 3.14, string _x_step = "default")
        {
            this.arg_a = _arg_a;
            this.arg_b = _arg_b;
            this.arg_c = _arg_c;
            if (_x_start < _x_end)
            {
                this.x_start = _x_start;
                this.x_end = _x_end;
            }
            else
            {
                this.x_start = _x_end;
                this.x_end = _x_start;
            }

            if (_x_step == "default")
                this.x_step = 0;
            else
                this.x_step = Convert.ToDouble(_x_step);
            x = x_start;

        }
        public void DoCollision(Frame nFrame, int interference_level)
        {
            if (this.x_step == 0) /* dla krkoku 0 ( cyzli domyslnego ) generuje korok w ktorym wszystkie argumenty funcji beda sie zawierac
                                  * w przedziale <-3.14; 3.14> dla danej funkcji
                                  */
            {

                int number_of_bits_in_frame = nFrame.GetCountInformationAndControlPart();
                this.x_step = Math.Abs(x_end - x_start) / number_of_bits_in_frame;
            }


            //double collision = Math.Abs(arg_a - ((double)interference_level / 1000) * arg_a);
            double collision = Math.Abs(arg_a - ((double)interference_level / 1000) * arg_a);
            for (int i = 0; i < nFrame.GetInformationPart().Count; i++, x += x_step)
            {
                if (Math.Abs(Function(x)) > collision) // dla wartosci 
                {
                    if (nFrame[i] == 0) // ustawia zawsze wartosc przeciwna w momencie wystapienia zaklocenia
                        nFrame[i] = 1;
                    else
                        nFrame[i] = 0;
                }

            }
            DoCollisionForControlPart(nFrame.GetControlPart(), interference_level);

        }
        public void DoCollision(Package nPackage, int interference_level)
        {
            if (this.x_step == 0) /* dla krkoku 0 ( cyzli domyslnego ) generuje korok w ktorym wszystkie argumenty funcji beda sie zawierac
                                  * w przedziale <-3.14; 3.14> dla danej funkcji
                                  */
            {
                int number_of_bits_in_package = 0;
                foreach (var item in nPackage.GetFrames())
                {
                    number_of_bits_in_package += item.GetCountInformationAndControlPart();
                }
                number_of_bits_in_package += nPackage.GetControlPart().GetList().Count; // dodaje liczbe elementow w czesci kontrolnej
                this.x_step = Math.Abs(x_end - x_start) / number_of_bits_in_package;
                //Console.WriteLine("STEP : " + this.x_step);


            }
            foreach (var item in nPackage.GetFrames())
            {
                DoCollision(item, interference_level);
            }
            DoCollisionForControlPart(nPackage.GetControlPart(), interference_level);
            this.x = x_start;

        }
        private void DoCollisionForControlPart(ControlElements ce, int interference_level)
        {
            for (int i = 0; i < ce.GetList().Count; i++, x += x_step)
            {
                if (Math.Abs(Function(x)) > (double)1 / interference_level) // dla wartosci 
                {
                    if (ce[i] == 0) // ustawia zawsze wartosc przeciwna w momencie wystapienia zaklocenia
                        ce[i] = 1;
                    else
                        ce[i] = 0;
                }
            }
        }
    }
}
