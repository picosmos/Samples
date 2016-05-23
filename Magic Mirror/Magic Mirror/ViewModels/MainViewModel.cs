using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.Web.Http;
using GalaSoft.MvvmLight;
using Koopakiller.Apps.MagicMirror.Model;
using Newtonsoft.Json.Linq;

namespace Koopakiller.Apps.MagicMirror.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _dt;
        private WeatherData _currentWeather;

        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.CurrentWeather = new WeatherData()
                {
                    Summary = "Clear",
                    Humidity = 0.5,
                    Temperature = Temperature.FromCelsius(22),
                };
            }
            else
            {
                this.UpdateWeatherData();
            }
            this._dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            this._dt.Tick += this.Dt_Tick;
            this._dt.Start();
        }

        private void Dt_Tick(object sender, object e)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanged(nameof(this.CurrentTime));
            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanged(nameof(this.CurrentTimeString));
            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanged(nameof(this.CurrentTimeSecondAngle));
            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanged(nameof(this.CurrentTimeMinuteAngle));
            // ReSharper disable once ExplicitCallerInfoArgument
            this.RaisePropertyChanged(nameof(this.CurrentTimeHourAngle));
        }

        public double CurrentTimeSecondAngle => DateTime.Now.Second * 6D;
        public double CurrentTimeMinuteAngle => (DateTime.Now.Minute * 60 + DateTime.Now.Second) / 3600D * 360D;
        public double CurrentTimeHourAngle => (DateTime.Now.Hour % 12 * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) / 86400D * 360D * 2;
        public DateTime CurrentTime => DateTime.Now;
        public string CurrentTimeString => DateTime.Now.ToString("hh:mm:ss");


        public WeatherData CurrentWeather
        {
            get { return this._currentWeather; }
            set
            {
                this._currentWeather = value;
                this.RaisePropertyChanged();
            }
        }

        public async Task UpdateWeatherData()
        {
            var lat = "51.170";
            var lon = "13.480";
            var language = "de";
            var units = "si";
            var url = $@"https://api.forecast.io/forecast/{SensitiveData.ForecastIoApiKey}/{lat},{lon}?lang={language}&units={units}";

            var wc = new HttpClient();
            var result = await wc.GetAsync(new Uri(url, UriKind.RelativeOrAbsolute));

            var jsondata = result.Content.ToString();

            var json = JObject.Parse(jsondata);
            var current = json["currently"];
            this.CurrentWeather = new WeatherData()
            {
                Summary = current["summary"].ToString(),
                Humidity = double.Parse(current["humidity"].ToString()),
                Temperature = Temperature.FromCelsius(double.Parse(current["temperature"].ToString()))
            };
        }
    }
}
