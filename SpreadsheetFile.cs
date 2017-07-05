using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace TemperatureApp
{
    public class SpreadsheetFile : FileType
    {
        //This is what will hold the objects (Records) that contain the data from the spreadsheet
        public List<Record> datalist;

        public List<Record> assembleData(SpreadsheetFile sf)
        {

            

            List<Record> dataList = new List<Record>();
            //int counter = 0;
            string[] x = File.ReadAllLines(pathname);
            foreach (var row in File.ReadAllLines(pathname))
            {
                //Debug.Write(row);
                //rowArray contains data in each specified row
                string[] rowArray = row.Split(',');
                Record rowObj = new Record(rowArray[0], rowArray[1], rowArray[2], rowArray[3]);
                dataList.Add(rowObj);
            }

            return dataList;
        }

        public static void displayData(SpreadsheetFile sf)
        {
            foreach(Record row in sf.datalist)
            {
                //Output contents of object to console
                Console.WriteLine($"{row.dateAndTime} \t {row.serialNo} \t {row.reading} \t {row.units}");
            }
        }
    }
}
