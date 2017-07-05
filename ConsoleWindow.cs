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
        public static void consoleRequest(SpreadsheetFile sf, string pathname)
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
                            read: display the data stored in the spreadsheet.
                            runtimer : Set up a timer for scheduling emails about the status of the thermometer.
                            display: Displays data collected so far.
                            email: recieve the data as an email.
                            reload: Reload data stored in the spreadsheet.
                            quit: Exit the program.");
                            break;
                        }

                    case "changetemp":
                        {
                            prompt = "Please enter the warning temperature you would like to set: ";
                            bool satisfied = false;
                            while (!satisfied)
                            {
                                Console.Write(prompt);
                                string temp = Console.ReadLine();
                                //NEEDS ERROR CHECKING
                                StreamWriter file = new StreamWriter(pathname);
                                file.WriteLine(temp);
                                Console.WriteLine(temp);
                                file.Close();
                                satisfied = true;
                                Console.WriteLine($"Teperature has been set to: {temp}°C");
                            }
                            break;
                        }
                    case "read":
                        {
                            SpreadsheetFile.displayData(sf);
                            break;
                        }
                    case "runtimer":
                        {
                            
                            TimerCheck.RunTimer(sf);
                            break;
                        }
                    case "email":
                        {
                            Console.Write("Please enter the email address you'd like the data to be sent to: ");
                            string receiver = Console.ReadLine();
                            email.composeEmail(sf, receiver);
                            break;
                        }
                    case "reload":
                        {
                            sf.datalist = sf.assembleData(sf);
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


