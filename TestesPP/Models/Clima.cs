using System;
using System.Collections.Generic;
using System.Text;

namespace TestesPP.Models
{
    public class Clima
    {
        public string DiaSemana { get; set; }
        public string Condicao { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public string Icon { get; set; }
        public bool EhHoje { get; set; }
    }
}
