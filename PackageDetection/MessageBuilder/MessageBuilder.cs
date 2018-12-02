using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageDetection.MessageBuilderPackage
{
    public static class MessageBuilder
    {
        private static StringBuilder sb = new StringBuilder();

        private static string FILE_PATH = @"C:\Users\lipin\source\repos\PackageDetection\PackageDetection\ResultsTxt\";

        public static void AddInfoMessage(string str)
        {
            sb.AppendLine("[INFO] : " + str + "\n");
        }
        public static void AddTitleMessage(string str)
        {
            sb.AppendLine("---------" + str + "---------\n\n");
            sb.AppendLine();
        }

        public static void AddMainTitleMessage(string str)
        {
            sb.AppendLine();
            sb.AppendLine("\n|**************" + str + "**************|  \n\n");
            sb.AppendLine();
            sb.AppendLine();
        }


        public static void AddWarnMessage(string str)
        {
            sb.AppendLine("[WARN] : " + str + "\n");
        }
        public static void AddErrorMessage(string str)
        {
            sb.AppendLine("[ERROR] : " + str + "\n");
        }
        public static void ClearMessage()
        {
            sb.Clear();
        }
        public static string GetMessage()
        {
            return sb.ToString();
        }

        public static void WriteMessageToFile(string fileName)
        {
            string fileNameWithPath = FILE_PATH + fileName + ".txt";

            using (StreamWriter sw = File.CreateText(fileNameWithPath))
            {
                sw.Write(sb.ToString());
                
            }

        }

        private static void Example()
        {
            // Create a StringBuilder that expects to hold 50 characters.
            // Initialize the StringBuilder with "ABC".
            StringBuilder sb = new StringBuilder("ABC", 50);

            // Append three characters (D, E, and F) to the end of the StringBuilder.
            sb.Append(new char[] { 'D', 'E', 'F' });

            // Append a format string to the end of the StringBuilder.
            sb.AppendFormat("GHI{0}{1}", 'J', 'k');

            // Display the number of characters in the StringBuilder and its string.
            Console.WriteLine("{0} chars: {1}", sb.Length, sb.ToString());

            // Insert a string at the beginning of the StringBuilder.
            sb.Insert(0, "Alphabet: ");

            // Replace all lowercase k's with uppercase K's.
            sb.Replace('k', 'K');
            sb.Clear();

            // Display the number of characters in the StringBuilder and its string.
            Console.WriteLine("{0} chars: {1}", sb.Length, sb.ToString());
        }
    }
}
