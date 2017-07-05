using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Mail;
using System.Net;

namespace TemperatureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hi");
            string pathname = ConsoleWindow.checkFile("Please enter the filepath of the data: ");
            //string pathname = @"C:\Temperature_app\test.csv";
            List<Record> dataList = new List<Record>();
            dataList = ConsoleWindow.displayData(pathname);
            string warningPathname;
            warningPathname = ConsoleWindow.checkFile("Please enter the file containing the warning temperature: ");
            ConsoleWindow.readWarnings(warningPathname, dataList);
            ConsoleWindow.consoleRequest(dataList);
            

        }
    }
}