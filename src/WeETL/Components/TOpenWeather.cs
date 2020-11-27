using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeETL.Schemas;

namespace WeETL.Components
{

    public class TOpenWeather : TRest<OpenWeatherMapSchema>
    {
        public TOpenWeather(IConfiguration cfg,ILogger<TOpenWeather> logger):base(logger)
        {
            cfg.GetSection(OPENWEATHERMAP_SECTION).Bind(this);
            Options.RequestUri = Options.RequestUri ?? DEFAULT_API_URI;
            
        }
        public const string OPENWEATHERMAP_SECTION = "OpenWeatherMap";
        public const string DEFAULT_API_URI = "http://api.openweathermap.org/data/{ApiVersion}/weather?q={City}&lang={Lang}&units={Units}&appid={ApiKey}";
        public string ApiKey { get; set; }
        public string City { get; set; }
        public string ApiVersion { get; set; } = "2.5";
        public string Lang { get; set; } = "fr";
        public string Units { get; set; } = "metric";

      /*  protected override string GetRequestUri()
        {
            var res = this.ToString(RequestUri);
            return res;
        }*/
    }

}
