using System.Collections.Generic;
using System.IO;

namespace TemperatureConverter
{
    /// <summary>
    /// Class for reading and writing to a file
    /// </summary>
    public class InOutUtil
    {
        /// <summary>
        /// Reads the data from the .csv file
        /// and adds the temperature scales
        /// to a list
        /// </summary>
        /// <param name="fn">Path of the file that
        /// is being read</param>
        /// <returns>A list that contains
        /// the temperature scales that
        /// are being saved in a .csv file</returns>
        public static List<TemperatureScale> Read(string fn)
        {
            int i;
            string name;
            char symbol;
            string line;
            List<TemperatureScale> scaleList = new List<TemperatureScale>();
            List<string> conversions;

            using (StreamReader read = new StreamReader(fn))
            {
                while ((line = read.ReadLine()) != null)
                {
                    conversions = new List<string>();
                    i = 2;
                    string[] parts = line.Split(',');
                    name = parts[0];
                    symbol = parts[1].ToCharArray()[0];
                    while (parts.Length > 2 && i < parts.Length)
                    {
                        conversions.Add(parts[i]);
                        i++;
                    }
                    TemperatureScale newscale = new TemperatureScale(name, symbol, conversions);
                    scaleList.Add(newscale);
                }
            }
            return scaleList;
        }
        /// <summary>
        /// Appends a temperature scale to the .csv file
        /// </summary>
        /// <param name="fn">Path of the file that
        /// data is being appended to</param>
        /// <param name="tempsc">Temperature
        /// scale data that is appended to
        /// the end of the .csv file</param>
        public static void AppendScale(string fn, TemperatureScale tempsc)
        {
            using (StreamWriter write = new StreamWriter(fn, true))
            {
                write.WriteLine(tempsc.ToString());
            }
        }
    }
}
