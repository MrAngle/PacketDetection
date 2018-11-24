using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    /*
     * Okresla w jaki sposob bity w pakietach maja zostac przeklamane
     */
    public interface ICollision
    {
        /// <summary>
        /// Przeklamuje bity ramki
        /// </summary>
        /// <param name="nFrame"></param>
        /// <param name="interference_level">poziom zaklocen (0-100000)</param>
        void DoCollision(Frame nFrame, int interference_level);
        /// <summary>
        /// Przeklamuje bity pakietu
        /// </summary>
        /// <param name="nPackage"></param>
        /// <param name="interference_level"></param>
        void DoCollision(Package nPackage, int interference_level);
    }
}
