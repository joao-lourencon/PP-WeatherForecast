using System;
using System.Collections.Generic;
using System.Text;

namespace TestesPP.Services
{
    using System.Net.Http.Json;
    using TestesPP.Models;

    public class BrasilApiService
    {
        private readonly HttpClient _http = new HttpClient();

        public async Task<string> GetCityByCep(string cep)
        {
            var url = $"https://brasilapi.com.br/api/cep/v1/{cep}";
            var result = await _http.GetFromJsonAsync<BrasilApiCepResponse>(url);
            return result.city;
        }

        public async Task<Endereco> GetEnderecoByCep(string cep)
        {
            try
            {
                var url = $"https://brasilapi.com.br/api/cep/v1/{cep.Replace("-", "")}";
                var endereco = await _http.GetFromJsonAsync<Endereco>(url);
                return endereco;
            }
            catch
            {
                return null;
            }
        }
    }

}
