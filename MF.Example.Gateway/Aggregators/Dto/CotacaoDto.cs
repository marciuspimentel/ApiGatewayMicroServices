using System;

namespace MF.Example.Gateway.Aggregators.Dto
{
    public class CotacaoDto
    {
        public string cotacao { get; set; }
        public string token { get; set; }
        public string cepOrigem { get; set; }
        public string cepDestino { get; set; }
        public int quantidade { get; set; }
        public string freteTipo { get; set; }
        public int prazo { get; set; }
        public double valor { get; set; }
    }
}
