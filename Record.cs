using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TemperatureApp
{
    public class Record
    {
        public string dateAndTime;
        public string serialNo;
        public string reading;
        public string units;
        public Record(string d, string s, string r, string u)

        {
            //Eliminate double quotes for output
            dateAndTime = Regex.Replace(d, "\"", "");
            serialNo = Regex.Replace(s, "\"", "");
            reading = Regex.Replace(r, "\"", "");
            units = Regex.Replace(u, "\"", "");
        }

    }
}
