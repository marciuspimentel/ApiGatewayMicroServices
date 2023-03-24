using Ocelot.Responses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Ocelot.Multiplexer;
using System.IO;
using System.Net.Http;
using Ocelot.Middleware;
using MF.Example.Gateway.Aggregators.Dto;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

public class ConcatenateAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        if (responses.Any(r => r.Items.Errors().Count > 0))
        {
            return new DownstreamResponse(null, HttpStatusCode.BadRequest, (List<Header>)null, null);
        }

        var aggregationResponse = new AggregationResponse();
        aggregationResponse.Cotacoes = new List<CotacaoDto>();

        await Task.WhenAll(responses.Select(async r =>
        {
            var jsonCotacao = await r.Items.DownstreamResponse().Content.ReadAsStringAsync();
            var cotacaoDto = JsonConvert.DeserializeObject<CotacaoDto>(jsonCotacao);
            aggregationResponse.Cotacoes.Add(cotacaoDto);
            aggregationResponse.ValorTotal += cotacaoDto.valor;
            aggregationResponse.PrazoTotal += cotacaoDto.prazo;
            aggregationResponse.QuantidadeTotal += cotacaoDto.quantidade;
        }));

        var retornoAgregate = JsonConvert.SerializeObject(aggregationResponse);
        var stringContent = new StringContent(retornoAgregate)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };
        return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

    }
}

public class AggregationResponse
{
    public List<CotacaoDto> Cotacoes { get; set; }
    public double ValorTotal { get; set; }
    public int PrazoTotal { get; set; }
    public int QuantidadeTotal { get; set; }
}