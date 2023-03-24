using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MF.Example.Correios.Dtos;

namespace MF.Example.Correios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacaoCorreiosController : ControllerBase
    {
        private static readonly double[] _valores = new[]
        {
            10.5,
            45.6,
            9.99,
            35.00
        };

        private static readonly int[] _prazos = new[]
        {
            1,
            2,
            3,
            4
        };

        private static readonly string[] _tipos = new[]
        {
            "Sedex",
            "PAC"
        };

        private readonly ILogger<CotacaoCorreiosController> _logger;

        public CotacaoCorreiosController(ILogger<CotacaoCorreiosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public CotacaoDto Get()
        {
            var rng = new Random();

            return new CotacaoDto
            {
                token = System.Guid.NewGuid().ToString(),
                cepOrigem = "01520-000",
                cepDestino = "484000-000",
                valor = _valores[rng.Next(_valores.Length)],
                prazo = _prazos[rng.Next(_prazos.Length)],
                cotacao = "Correios",
                freteTipo = _tipos[rng.Next(_tipos.Length)]
            };
        }
    }
}