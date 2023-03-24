using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MF.Example.Geral.Dtos;
using System;

namespace MF.Example.Geral.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacaoController : ControllerBase
    {
        private static readonly double[] _valores = new[]
        {
            15.5,
            10,
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
            "Expressa",
            "Economica"            
        };

        private readonly ILogger<CotacaoController> _logger;

        public CotacaoController(ILogger<CotacaoController> logger)
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
                cotacao = "Geral",
                freteTipo = _tipos[rng.Next(_tipos.Length)]                
            };
        }
    }
}