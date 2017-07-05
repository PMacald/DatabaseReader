using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TemperatureApp 
{
    public class WarningFile : FileType
    {
        public string temperature;

        public string getTemp(string path)
        {
            string temp = "";
            using (StreamReader sr = new StreamReader(path))
            {
                temp = sr.ReadToEnd();
            }

            return temp;
        }


        public void readWarnings(SpreadsheetFile sf, WarningFile wf)
        {

            //Get temperature at which a warning will be produced
            wf.temperature = wf.getTemp(wf.pathname);

            float tempFloat = float.Parse(wf.temperature);

            string timeOfError = "";
            //Console.Write(dataList);
            bool warning = false;
            foreach (var dataRow in sf.datalist)
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
                string output = Regex.Replace(wf.temperature, "[\r,\n]", "");
                Console.WriteLine($"A temperature of over {output}°C has been Recorded. It was Recorded at {timeOfError}");
            }
        }
    }
}
