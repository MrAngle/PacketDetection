using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    public class RandomCollision : ICollision
    {
        public void DoCollision(Frame nFrame, int interference_level)
        {
            for (int i = 0; i < nFrame.GetInformationPart().Count; i++)
            {
                if (interference_level > Helpers.GenerateRandomNumber(0, 10000)) // losuje liczbe z przedzialu 0,9999. Im 
                                                                                   // im wieksze zaklocenia (interference_lvl)
                                                                                   // tym wieksza szansa na przeklamanie
                {
                    if (nFrame[i] == 0) // ustawia zawsze wartosc przeciwna w momencie wystapienia zaklocenia
                        nFrame[i] = 1;
                    else
                        nFrame[i] = 0;
                }
            }
            DoCollisionForControlElements(nFrame.GetControlPart(), interference_level); // generuje losowe wartosci dla ControlElements


        }
        private void DoCollisionForControlElements(ControlElements ce, int interference_level)
        {
            for (int i = 0; i < ce.GetList().Count; i++)
            {
                if (interference_level > Helpers.GenerateRandomNumber(0, 10000))
                {
                    if (ce[i] == 0)
                        ce[i] = 1;
                    else
                        ce[i] = 0;
                }
            }
        }

        public void DoCollision(Package nPackage, int interference_level)
        {
            for (int i = 0; i < nPackage.GetFrames().Count; i++)
            {
                DoCollision(nPackage[i], interference_level);
            }
            DoCollisionForControlElements(nPackage.GetControlPart(), interference_level);
        }
    }
}
