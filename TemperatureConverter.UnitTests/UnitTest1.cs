using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TemperatureConverter.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        public string testData = "..\\..\\..\\TestData.csv";
        [TestMethod]
        public void Reading_File_Adds_Objects_To_List()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);

            foreach (TemperatureScale scale in list)
            {
                Assert.IsTrue(scale.GetConversionFormulaList().Count == 3);
            }

            Assert.IsTrue(list.Count == 3);
        }

        [TestMethod]
        public void Check_If_Conversion_Is_Achieved_Celsius_To_Kelvin()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);
            double temperature = 10.0;

            /*
             * 1. Celsius
             * 2. Kelvin
             * 3. Fahrenheit
             */

            double result = Program.Conversion(0, 1, list, temperature);

            Assert.IsTrue(Equals(result, 283.15));
        }

        [TestMethod]
        public void Check_If_Conversion_Is_Achieved_Celsius_To_Fahrenheit()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);
            double temperature = 10.0;

            /*
             * 1. Celsius
             * 2. Kelvin
             * 3. Fahrenheit
             */

            double result = Program.Conversion(0, 2, list, temperature);

            Assert.IsTrue(Equals(result, 50.0));
        }

        [TestMethod]
        public void Check_If_Conversion_Is_Achieved_Kelvin_To_Celsius()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);
            double temperature = 10.0;

            /*
             * 1. Celsius
             * 2. Kelvin
             * 3. Fahrenheit
             */

            double result = Program.Conversion(1, 0, list, temperature);

            Assert.IsTrue(Equals(result, -263.15));
        }

        [TestMethod]
        public void Check_If_Conversion_Is_Achieved_Kelvin_To_Fahrenheit()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);
            double temperature = 10.0;

            /*
             * 1. Celsius
             * 2. Kelvin
             * 3. Fahrenheit
             */

            double result = Program.Conversion(1, 2, list, temperature);

            Assert.IsTrue(Equals(result, -441.67));
        }

        [TestMethod]
        public void Check_If_Conversion_Is_Achieved_Fahrenheit_To_Celsius()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);
            double temperature = 10.0;

            /*
             * 1. Celsius
             * 2. Kelvin
             * 3. Fahrenheit
             */

            double result = Program.Conversion(2, 0, list, temperature);

            Assert.IsTrue(Equals(result, -12.22));
        }

        [TestMethod]
        public void Check_If_Conversion_Is_Achieved_Fahrenheit_To_Kelvin()
        {
            List<TemperatureScale> list = InOutUtil.Read(testData);
            double temperature = 10.0;

            /*
             * 1. Celsius
             * 2. Kelvin
             * 3. Fahrenheit
             */

            double result = Program.Conversion(2, 1, list, temperature);

            Assert.IsTrue(Equals(result, 260.93));
        }
    }
}
