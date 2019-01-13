using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko
{
    // Funkcja wykrywania bledu zwraca (Collision Detection): 
    // 0 - w przypadku gdy nie ma błędu i dany system sprawdzający też stwierdził ze nie ma błędu
    // 1 - gdy wykryto błąd i ten błąd rzeczywiscie występował
    // 2 - gdy nie wykryto błędu, a dany bład występował
    // 3 - gdy wykryto błąd, a błąd nie występował ( przy ograniczeniach dlugosci części kontrolnej i 
    //                                              do testowania poprawnosci dzialania algorytmu kontroli )
    /*
     * 
     * przydatne funkcje:
     * fra.GetInformationPart() - zwraca listę częsci informacyjnej ( bez części kontroli )
     * fra.GetControlPart() - zwraca obiekt typu ControlElements
     * fra.GetControlPart().GetList() - zwraca listę z elementami częsci kontrolnej
     * fra.GetControlPart().GetCRCDivider() - zwraca dzielnik dla CRC ( DZIALA TYLKO DLA TYPU KONTORLI CRC )
     * mozesz tez odowoływać się za pomoca []. Opis znajduje sie w klasie IFrame i IPackage
     * 
     */

    public interface IControl
    {
        /// <summary>
        /// Obliczanie czesci kontrolnej dla Ramki
        /// </summary>
        /// <param name="nFrame"></param>
        /// <param name="sizeOfControlPart">Okresla dlugosc czesci kontrolnej. FLEXIBLE automatycznie okresla dlugosc czesci kontrolnej</param>
        /// <returns></returns>
        List<byte> CalculateControlPart(Frame nFrame, int sizeOfControlPart = Helpers.FLEXIBLE);
        /// <summary>
        /// Obliczanie czesci kontrolnej dla Pakietu
        /// </summary>
        /// <param name="nFrame"></param>
        /// <param name="sizeOfControlPart">Okresla dlugosc czesci kontrolnej. FLEXIBLE automatycznie okresla dlugosc czesci kontrolnej</param>
        /// <returns></returns>
        List<byte> CalculateControlPart(Package nPakiet, int sizeOfControlPart = Helpers.FLEXIBLE);

        /// <summary>
        /// Wykrywanie przeklaman w ramce
        /// </summary>
        /// <param name="nFrame"></param>
        /// <returns></returns>
        byte CollisionDetection(Frame nFrame);
        /// <summary>
        /// Wykrywanie przeklaman w pakiecie
        /// </summary>
        /// <param name="nFrame"></param>
        /// <returns></returns>
        byte CollisionDetection(Package nPackage);
    }
}
