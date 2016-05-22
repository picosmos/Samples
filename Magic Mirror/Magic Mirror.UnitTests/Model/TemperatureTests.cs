using System;
using Koopakiller.Apps.MagicMirror.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Koopakiller.Apps.MagicMirror.UnitTests.Model
{
    [TestClass]
    public class TemperatureTests
    {
        [TestMethod]
        public void Constructor()
        {
            var temp = new Temperature();
            this.Check(temp, 0D);
        }

        [TestMethod]
        public void FromCelsius()
        {
            var temp = Temperature.FromCelsius(0);
            this.Check(temp, 0D);
            temp = Temperature.FromCelsius(100);
            this.Check(temp, 100D);
        }

        [TestMethod]
        public void FromDelisle()
        {
            var temp = Temperature.FromDelisle(0);
            this.Check(temp, 100 - 0D * 2 / 3);
            temp = Temperature.FromDelisle(100);
            this.Check(temp, 100 - 100D * 2 / 3);
        }

        [TestMethod]
        public void FromFahrenheit()
        {
            var temp = Temperature.FromFahrenheit(0);
            this.Check(temp, (0D - 32) * 5 / 9);
            temp = Temperature.FromFahrenheit(100);
            this.Check(temp, (100D - 32) * 5 / 9);
        }

        [TestMethod]
        public void FromKelvin()
        {
            var temp = Temperature.FromKelvin(0);
            this.Check(temp, 0D - 273.15);
            temp = Temperature.FromKelvin(100);
            this.Check(temp, 100D - 273.15);
        }

        [TestMethod]
        public void FromNewton()
        {
            var temp = Temperature.FromNewton(0);
            this.Check(temp, 0D * 100 / 33);
            temp = Temperature.FromNewton(100);
            this.Check(temp, 100D * 100 / 33);
        }

        [TestMethod]
        public void FromRankine()
        {
            var temp = Temperature.FromRankine(0);
            this.Check(temp, (0D - 491.67) * 5 / 9);
            temp = Temperature.FromRankine(100);
            this.Check(temp, (100D - 491.67) * 5 / 9);
        }

        [TestMethod]
        public void FromReaumur()
        {
            var temp = Temperature.FromReaumur(0);
            this.Check(temp, 0D * 5 / 4);
            temp = Temperature.FromReaumur(100);
            this.Check(temp, 100D * 5 / 4);
        }

        [TestMethod]
        public void FromRomer()
        {
            var temp = Temperature.FromRomer(0);
            this.Check(temp, (0D - 7.5) * 40 / 21);
            temp = Temperature.FromRomer(100);
            this.Check(temp, (100D - 7.5) * 40 / 21);
        }

        private void Check(Temperature temp, double celsius)
        {
            var delta = Math.Abs(celsius) * 1E-14;
            Assert.AreEqual(celsius, temp.Celsius, delta, $"Failed at {nameof(temp.Celsius)}");
            Assert.AreEqual((100 - celsius) * 3 / 2, temp.Delisle, delta, $"Failed at {nameof(temp.Delisle)}");
            Assert.AreEqual(celsius * 9 / 5 + 32, temp.Fahrenheit, delta, $"Failed at {nameof(temp.Fahrenheit)}");
            Assert.AreEqual(celsius + 273.15, temp.Kelvin, delta, $"Failed at {nameof(temp.Kelvin)}");
            Assert.AreEqual(celsius * 33 / 100, temp.Newton, delta, $"Failed at {nameof(temp.Newton)}");
            Assert.AreEqual((celsius + 273.15) * 9 / 5, temp.Rankine, delta, $"Failed at {nameof(temp.Rankine)}");
            Assert.AreEqual(celsius * 4 / 5, temp.Reaumur, delta, $"Failed at {nameof(temp.Reaumur)}");
            Assert.AreEqual(celsius * 21 / 40 + 7.5, temp.Romer, delta, $"Failed at {nameof(temp.Romer)}");
        }
    }
}
