using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TemperatureApp
{
    public class FileType
    {
        public string pathname;
        public string checkFile(string prompt)
        {

            bool fileFound = false;
            string pathname = "";
            while (!fileFound)
            {
                //ask for pathname
                Console.WriteLine(prompt);
                pathname = Console.ReadLine();
                //check file exists at specified location
                if (File.Exists(pathname) ? true : false)
                {
                    Console.WriteLine("File has been found");

                    fileFound = true;
                }
                else
                {
                    prompt = "Sorry, that file has not been found. Please try again: ";
                }

            }

            return pathname;
        }
    }
}
