using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using TestesPP.Models;

namespace TestesPP.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly string apiKey = "9efd48b0f66041a8aac120409250812";

        public async Task<List<Clima>> GetForecast(string city)
        {
            var url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={city}&days=4&lang=pt";
            var result = await _http.GetFromJsonAsync<WeatherApiResponse>(url);

            var lista = new List<Clima>();

            lista.Add(new Clima
            {
                Condicao = result.current.condition.text,
                TempAtual = result.current.temp_c,
                TempMax = result.forecast.forecastday[0].day.maxtemp_c,
                TempMin = result.forecast.forecastday[0].day.mintemp_c,
                Icon = "https:" + result.current.condition.icon
            });

            for (int i = 1; i < result.forecast.forecastday.Count; i++)
            {
                var day = result.forecast.forecastday[i];

                lista.Add(new Clima
                {
                    Condicao = day.day.condition.text,
                    TempAtual = double.NaN,
                    TempMax = day.day.maxtemp_c,
                    TempMin = day.day.mintemp_c,
                    Icon = "https:" + day.day.condition.icon
                });
            }

            return lista;
        }
    }
}
