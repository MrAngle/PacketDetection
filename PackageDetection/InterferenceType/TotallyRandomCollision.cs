using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    class TotallyRandomCollision 
    {
        public void DoCollision(Frame nFrame, int interference_level)
        {
            for (int i = 0; i < nFrame.GetInformationPart().Count; i++)
            {
                if (interference_level > Helpers.GenerateRandomNumber(0, 10000))// losuje liczbe z przedzialu 0,9999. Im 
                                                                                  // im wieksze zaklocenia (interference_lvl)
                                                                                  // tym wieksza szansa na przeklamanie
                {
                    nFrame[i] = Helpers.GenerateRandomByte();     // nie musi zamienic wartosci (losuje 1 albo 0 )
                }
            }
            DoCollisionForControlElements(nFrame.GetControlPart(), interference_level);
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
    }
}
