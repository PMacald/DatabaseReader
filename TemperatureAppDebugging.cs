using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace TemperatureApp

{
    public class record
    {
        public string dateAndTime { get; set; }
        public string serialNo { get; set; }
        public string reading { get; set; }
        public string units { get; set; }
        public void getValues(string d, string s, string r, string u)
        {
            //Eliminate double quotes for output
            dateAndTime = Regex.Replace(d, "\"", "");
            serialNo = Regex.Replace(s, "\"", "");
            reading = Regex.Replace(r, "\"", "");
            units = Regex.Replace(u, "\"", "");
        }
    }

    public class ConsoleWindow
    {
        public static string checkFile() {
            string prompt = "Please enter the filepath of the data: ";
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

        public static List<record> displayData(string pathname)
        {
            
            List<string> lines = new List<string>();
            
            List<record> dataList = new List<record>();
            //int counter = 0;
            foreach (var row in File.ReadAllLines(pathname))
            {
                //Debug.Write(row);
                lines.Add(row);
                //Console.Write(row + "\n");
                //rowArray contains data in each specified row, in array format
                string[] rowArray = row.Split(',');

                record rowObj = new record();

                //Call on method to fill row's data structure with record's column entries
                rowObj.getValues(rowArray[0], rowArray[1], rowArray[2], rowArray[3]);
                Console.WriteLine("{0} \t {1} \t {2} \t {3}", rowObj.dateAndTime, rowObj.serialNo, rowObj.reading, rowObj.units);

                dataList.Add(rowObj);


            }

            return dataList;
        }

        
        public static void consoleRequest()
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
                            changetemp : Allows you to input a new temperature at which a warning will be produced
                            
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
                                if (float.Parse(temp) <= 0)
                                {
                                    Console.WriteLine("This is an invalid temperature. Please try again");
                                }
                                else
                                {

                                    StreamWriter file = new StreamWriter(@"C:\Temperature_app\WarningTemp.txt");
                                    file.WriteLine(temp);
                                    Console.WriteLine(temp);
                                    file.Close();
                                    satisfied = true;
                                }
                            }
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






    class Program
    {
        static void Main(string[] args)
        {
            string pathname = ConsoleWindow.checkFile();
            //string pathname = @"C:\Temperature_app\test.csv";
            List<record> dataList = new List<record>();
            dataList = ConsoleWindow.displayData(pathname);
            

            string warningTemp;
            //Get temperature at which a warning will be produced
            using (StreamReader sr = new StreamReader(@"C:\Temperature_app\WarningTemp.txt"))
            {
                warningTemp = sr.ReadToEnd();
                Console.WriteLine(warningTemp);
            }

            float tempFloat = float.Parse(warningTemp);

            Console.WriteLine(tempFloat.GetType());
            string timeOfError = "";
            //Console.Write(dataList);
            bool error = false;
            foreach (var dataRow in dataList)
            {
                try
                {
                    if (Convert.ToDouble(dataRow.reading) > tempFloat)
                    {
                        error = true;
                        timeOfError = dataRow.dateAndTime;
                        break;
                    }
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            if (error)
            {
                Console.WriteLine("A temperature of over {0} degrees has been recorded. It was recorded at {1}", warningTemp, timeOfError);
            }

            ConsoleWindow.consoleRequest();

            Console.ReadKey();
        }
    }
}