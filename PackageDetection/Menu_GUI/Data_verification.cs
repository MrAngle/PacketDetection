using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_GUI
{
    public class Data_verification
    {
        public class HighException : System.Exception { }
        public static string Check(string text, long maximum_number, long minimum_number = 0, int max_char_number=3)
        {
            if (long.TryParse(text, out long number))
            {
                if (number > maximum_number)
                {
                    return maximum_number.ToString();
                }
                else
                {
                    if (number < minimum_number)
                    {
                        return minimum_number.ToString();
                    }
                    else
                    if (text.Count() > max_char_number)
                        return maximum_number.ToString();
                }
                return text;
            }
            else
            {
                return null;
            }
        }
        //public static bool Check_number_is_power_of_two(int number)
        //{
        //    int current_number = 0;
        //    if (number == 0)
        //        return true;
        //    //Console.WriteLine("teraz numer = " + number + ".");
        //    for (int i = 7; i >= 0; i--)
        //    {
        //        current_number += (int)Math.Pow(2, i);
        //        Console.WriteLine(current_number);
        //        if (number == current_number)
        //        {
        //            return true;
        //        }
        //    }

        //    throw new HighException();

        //}
    }
    
}
