using Grpc.Core;
using MF.Example.Grpc.Protos;

namespace MF.Example.Grpc.Services
{
    public class CotacaoService : CotacaoProtoService.CotacaoProtoServiceBase
    {
        public CotacaoService()
        {

        }

        public override Task<CotacaoDto> GetCotacao(GetCotacaoRequest request, ServerCallContext context)
        {
            var cotacao = new CotacaoDto
            {
                Cotacao = "GRPC",
                Token = request.Token.ToString(),
                CepOrigem = "01520-000",
                CepDestino = "01530-000",
                FreteTipo = "Expresa GRPC",
                Prazo = 1,
                Valor = 3.4,
                Quantidade = 1
            };

            return Task.FromResult(cotacao);
        }
    }
}
