using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeather(double Latitude  , double longitude)
        {
            var httpClient = new HttpClient();
           var response = await httpClient.GetStringAsync(string.Format("https:// api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=ac64edefd7aa9038971694033c4efc2c", Latitude, longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }
        public static async Task<Root> GetWeatherBigCity(string city )
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https:// api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=ac64edefd7aa9038971694033c4efc2c", city));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
