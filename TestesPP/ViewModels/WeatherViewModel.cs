using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TestesPP.Models;
using TestesPP.Services;

namespace TestesPP.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        private readonly BrasilApiService brasilApi = new();
        private readonly WeatherApiService weatherApi = new();

        [ObservableProperty]
        public string cep;

        [ObservableProperty]
        private Clima climaHoje;

        public ObservableCollection<Clima> Previsoes { get; set; }
            = new ObservableCollection<Clima>();

        public ICommand BuscarCommand => new Command(async () => await Buscar());

        private async Task Buscar()
        {
            var cidade = await brasilApi.GetCityByCep(cep);
            var previsao = await weatherApi.GetForecast(cidade);

            Previsoes.Clear();
            foreach (var p in previsao)
                Previsoes.Add(p);
        }
    }
}
