using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TemperatureConverter
{
    public class Program
    {
        //The file that temperature scales are stored in
        static string FILENAME = "..\\..\\TempScales.csv";

        static void Main()
        {
            // Uncomment if you wish to clear the temperature scales that
            // are present in the dataset
            // File.WriteAllText(FILENAME, null);

            // List that keeps the data of our dataset that is located
            // in a .csv file
            List<TemperatureScale> scaleList = InOutUtil.Read(FILENAME);

            Console.WriteLine("Do you wish to add a new temperature? (Y/N)");
            string ans = Console.ReadLine();

            // If the user enters the character 'y', the program proceeds to ask
            // for the specifics of the newly added scale
            if (string.Equals(ans, "y", StringComparison.CurrentCultureIgnoreCase))
            {
                // Array used for the temperature scale that is
                // being created to store conversion formulas
                // to the temperature scales that already
                // exist in the dataset
                List<string> formulas = new List<string>();

                Console.WriteLine("Insert the name");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("The name cannot be blank");
                    return;
                }

                Console.WriteLine("Insert the one character abbreviation");
                char abbChar = Console.ReadLine()[0];
                if (string.IsNullOrWhiteSpace(abbChar.ToString()))
                {
                    Console.WriteLine("The abbreviation cannot be blank");
                    return;
                }

                Console.WriteLine("Write the temperature variable name as 'a'");

                // A conversion formula is added to each temperature scale 
                // that is in the existing dataset
                foreach (TemperatureScale scales in scaleList)
                {
                    Console.WriteLine("Add a formula to convert to {0}", scales.Name);
                    formulas.Add(Console.ReadLine());
                }

                // Newly created temperature scale is added to the temperature scale
                // list
                scaleList.Add(new TemperatureScale(name, abbChar, formulas));

                Console.WriteLine("Add conversion from each of the temperature scales\n");

                // A conversion formula to the newly added temperature scale is added
                // to each conversion formula list that exists in the dataset
                foreach (TemperatureScale scales in scaleList)
                {
                    Console.WriteLine("Add a formula to convert from {0} to {1}",
                        scales.Name, scaleList[scaleList.Count - 1].Name);
                    scales.GetConversionFormulaList().Add(Console.ReadLine());
                }

                // Remove all of the old data that exists in the dataset
                // file and add the updated data
                File.WriteAllText(FILENAME, null);
                foreach (TemperatureScale scales in scaleList)
                {
                    InOutUtil.AppendScale(FILENAME, scales);
                }
            }

            Console.WriteLine("Enter the temperature value:");
            double tempValue = double.Parse(Console.ReadLine());

            // Prints the indicises and names of temperature scales in the dataset
            int index = 0;
            foreach (TemperatureScale scale in scaleList)
            {
                index++;
                Console.WriteLine(index + "." + " " + scale.Name);
            }

            // Since each temperature scale is linked to a list of
            // conversion formulas that are listed in the same order
            // as the temperature scales, only the index of the temperature
            // scale that the temperature is converted from and the index of
            // the temperature scale that it is being converted to is needed
            // to produce the final result

            Console.WriteLine("Paste the index of the scale you wish to convert from:");
            int fromIndex = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Paste the index of the scale you wish to convert to:");
            int toIndex = int.Parse(Console.ReadLine()) - 1;

            //Check if the user input is valid
            if (0 > fromIndex || scaleList.Count - 1 < fromIndex
               || 0 > toIndex || scaleList.Count - 1 < toIndex)
            {
                Console.WriteLine("The indeces of your choice " +
                                  "have to be between\nor equal to 1 and {0}",
                                  scaleList.Count);
                return;
            }

            double conversionResult = Conversion(fromIndex, toIndex, scaleList, tempValue);
            Console.WriteLine("The conversion result:" + conversionResult);
        }
        /// <summary>
        /// Method to acquire the stored formula in the dataset and use it to produce the
        /// final conversion
        /// </summary>
        /// <param name="fromIndex">Index to get the temperature scale
        /// that the temperature value is being converted from</param>
        /// <param name="toIndex">Index to get the formula to convert
        /// to the temperature scale that was requested</param>
        /// <param name="scales">List of the existing dataset that 
        /// contains the inserted temperature scales</param>
        /// <param name="temp">The temperature value that is being converted</param>
        /// <returns>The resulting conversion value upon success, 
        /// NaN if the conversion formula string had errors</returns>
        public static double Conversion(int fromIndex, int toIndex, List<TemperatureScale> scales, double temp)
        {
            //Converting the string formula to a mathematical expression
            string conversionFormula = scales[fromIndex].GetFormula(toIndex);
            //Evaluating the result
            string formulaWithoutVar = conversionFormula.Replace("a", temp.ToString());
            double result = 0.0;

            try
            {
                result = Convert.ToDouble(new DataTable().Compute(formulaWithoutVar, null));
            }
            catch
            {
                Console.WriteLine("The entered conversion formula to {0} was " +
                                  "entered incorrectly", scales[toIndex].Name);

                System.Environment.Exit(0);
            }

            return Math.Round(result, 2);
        }
    }
}
