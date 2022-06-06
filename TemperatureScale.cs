using System;
using System.Collections.Generic;

namespace TemperatureConverter
{
    public class TemperatureScale
    {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public List<string> ConversionFormulas { get; set; }

        /// <summary>
        /// Temperature scale constructor
        /// </summary>
        /// <param name="name">Name of the temperature scale</param>
        /// <param name="symbol">Abbreviation symbol of the temperature scale</param>
        /// <param name="conversion">Conversion formula list of temperature scale</param>
        public TemperatureScale(string name, char symbol, List<string> conversion)
        {
            Name = name;
            Symbol = symbol;
            ConversionFormulas = conversion;
        }

        /// <summary>
        /// Gets the conversion fomula list
        /// </summary>
        /// <returns>List of the conversion formulas</returns>
        public List<string> GetConversionFormulaList()
        {
            return ConversionFormulas;
        }
        /// <summary>
        /// Gets a specified formula by index
        /// </summary>
        /// <param name="i">Index of a formula</param>
        /// <returns>Formula string specified by index i</returns>
        public string GetFormula(int i)
        {
            return ConversionFormulas[i];
        }
        /// <summary>
        /// Overrides the ToString method
        /// </summary>
        /// <returns>Modified string sequence
        /// of the TempScale type</returns>
        public override string ToString()
        {
            String formulasString = "";

            foreach (string formula in ConversionFormulas)
            {
                formulasString = formulasString + "," + formula;
            }

            return Name + "," + Symbol + formulasString;
        }
    }
}
