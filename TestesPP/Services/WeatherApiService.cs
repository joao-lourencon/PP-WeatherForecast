using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using TestesPP.Models;

namespace TestesPP.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly string apiKey = "";

        public async Task<List<Clima>> GetForecast(string city)
        {


            var lista = new List<Clima>();

            try
            {
                var url =
                    $"https://api.weatherapi.com/v1/forecast.json" +
                    $"?key={apiKey}&q={city}&days=4&lang=pt";

                var result = await _http.GetFromJsonAsync<WeatherApiResponse>(url);


                if (result?.forecast?.forecastday == null)
                    return lista;

                var cultura = new CultureInfo("pt-BR");
                int index = 0;

                foreach (var day in result.forecast.forecastday)
                {

                    DateTime data = DateTime.Now.Date.AddDays(index);

                    var dia = cultura.TextInfo.ToTitleCase(
                        data.ToString("dddd", cultura)
                    );

                    lista.Add(new Clima
                    {
                        DiaSemana = dia,
                        Condicao = day.day.condition.text,
                        TempMax = day.day.maxtemp_c,
                        TempMin = day.day.mintemp_c,
                        Icon = "https:" + day.day.condition.icon,
                        EhHoje = index == 0
                    });

                    index++;
                }
            }
            catch
            {

            }

            return lista;
        }
    }
}
