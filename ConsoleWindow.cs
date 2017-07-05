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
    public class ConsoleWindow
    {
        public static string checkFile(string prompt)
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

        public static List<Record> displayData(string pathname)
        {

            List<string> lines = new List<string>();

            List<Record> dataList = new List<Record>();
            //int counter = 0;
            foreach (var row in File.ReadAllLines(pathname))
            {
                //Debug.Write(row);
                lines.Add(row);
                
                //rowArray contains data in each specified row, in Record format
                string[] rowArray = row.Split(',');

                Record rowObj = new Record(rowArray[0], rowArray[1], rowArray[2], rowArray[3]);

                //Output contents of object to console
                Console.WriteLine($"{rowObj.dateAndTime} \t {rowObj.serialNo} \t {rowObj.reading} \t {rowObj.units}");

                dataList.Add(rowObj);


            }

            return dataList;
        }

        public static void readWarnings(string pathname, List<Record> dataList)
        {
            string warningTemp;
            //Get temperature at which a warning will be produced
            using (StreamReader sr = new StreamReader(pathname))
            {
                warningTemp = sr.ReadToEnd();
                //Console.WriteLine(warningTemp);
            }

            float tempFloat = float.Parse(warningTemp);

            string timeOfError = "";
            //Console.Write(dataList);
            bool warning = false;
            foreach (var dataRow in dataList)
            {
                try
                {
                    //check if temperatures are greater than the warning level
                    if (Convert.ToDouble(dataRow.reading) > tempFloat)
                    {
                        warning = true;
                        timeOfError = dataRow.dateAndTime;
                        break;
                    }
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            if (warning)
            {
                Console.WriteLine($@"A temperature of over {Regex.Replace(warningTemp,"\n","")}°C has been Recorded. It was Recorded at {timeOfError}");
            }
        }

        public static void consoleRequest(List<Record> datalist)
        {
            bool finished = false;
            string prompt = "";
            while (!finished)
            {
                Console.WriteLine("What would you like to do? Type 'help' for more information");
                string userResponse = Console.ReadLine();
                switch (userResponse)
                {
                    case "help":
                        {
                            Console.Write(@"Commands you can use: 
                            changetemp : Allows you to input a new temperature at which a warning will be produced.
                            runtimer : Set up a timer for scheduling.
                            display: Displays data collected so far.
                            quit: Exit the program.
                            ");
                            break;
                        }

                    case "changetemp":
                        {
                            prompt = "Please enter the warning temperature you would like to set: ";
                            bool satisfied = false;
                            while (!satisfied)
                            {
                                Console.WriteLine(prompt);
                                string temp = Console.ReadLine();
                                float result = 0;
                                if (Single.TryParse(temp, out result))
                                {

                                }
                                else
                                {

                                    StreamWriter file = new StreamWriter(@"C:\Temperature_app\WarningTemp.txt");
                                    file.WriteLine(temp);
                                    Console.WriteLine(temp);
                                    file.Close();
                                    satisfied = true;
                                    Console.WriteLine($"Teperature has been set to: {temp}°C");
                                }
                            }
                            break;
                        }
                    case "runtimer":
                        {
                            Console.WriteLine("What value would you like the timer to have (in minutes)?");
                            float time = float.Parse(Console.ReadLine()) * 60000;
                            TimerCheck.RunTimer(time, datalist);
                            break;
                        }
                    case "quit": {
                            Environment.Exit(0);
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Sorry, that command is invalid. Please try again. Note: Type 'help' for more information.");
                            break;
                        }
                }
            }
        }
    }
}


