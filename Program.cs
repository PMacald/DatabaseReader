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
            SpreadsheetFile sf = new SpreadsheetFile();
            sf.pathname = sf.checkFile("Please enter the filepath of the data: ");
            sf.datalist = sf.assembleData(sf);
            WarningFile wf = new WarningFile();
            wf.pathname = wf.checkFile("Please enter the file containing the warning temperature: ");
            wf.readWarnings(sf, wf);
            ConsoleWindow.consoleRequest(sf, wf.pathname);
        }
    }
}