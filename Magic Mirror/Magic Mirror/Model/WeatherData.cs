using System;
using Newtonsoft.Json.Linq;

namespace Koopakiller.Apps.MagicMirror.Model
{
    public class WeatherData
    {
        public string Summary { get; set; }

        public double Humidity { get; set; }

        public Temperature Temperature { get; set; }
    }
}
