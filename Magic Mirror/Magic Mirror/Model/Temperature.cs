namespace Koopakiller.Apps.MagicMirror.Model
{
    /// <summary>
    /// Provides methods to store and convert a temperature in different measurements.
    /// </summary>
    /// <seealso href="https://en.wikipedia.org/wiki/Temperature#Conversion"/>
    public struct Temperature
    {
        private Temperature(double degreeCelsius)
        {
            this.Celsius = degreeCelsius;
        }

        public double Celsius { get; }
        public double Fahrenheit => this.Celsius * 1.8 + 32;
        public double Kelvin => this.Celsius + 273.15;
        public double Rankine => (this.Celsius+ 273.15) * 1.8;
        public double Delisle => (100 - this.Celsius) * 1.5;
        public double Newton => this.Celsius * 0.33;
        public double Reaumur => this.Celsius * 0.8;
        public double Romer => this.Celsius * 0.525 + 7.5;

        public static Temperature FromCelsius(double value) => new Temperature(value);
        public static Temperature FromFahrenheit(double value) => new Temperature((value - 32) * 5 / 9);
        public static Temperature FromKelvin(double value) => new Temperature(value - 273.15);
        public static Temperature FromRankine(double value) => new Temperature((value - 491.67) * 5 / 9);
        public static Temperature FromDelisle(double value) => new Temperature(100 - value * 2 / 3);
        public static Temperature FromNewton(double value) => new Temperature(value * 100 / 33);
        public static Temperature FromReaumur(double value) => new Temperature(value * 1.25);
        public static Temperature FromRomer(double value) => new Temperature((value - 7.5) * 40 / 21);
    }
}