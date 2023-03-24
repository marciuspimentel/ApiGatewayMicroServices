using MF.Example.Grpc.Protos;
using System.Threading.Tasks;

namespace MF.Example.Gateway.Services
{
    public class GrpcService
    {
        private readonly CotacaoProtoService.CotacaoProtoServiceClient _client;

        public GrpcService(CotacaoProtoService.CotacaoProtoServiceClient client)
        {
            _client = client;
        }

        public async Task<CotacaoDto> GetCotacao(string token)
        {
            var cotacaoRequest = new GetCotacaoRequest { Token = token };

            return await _client.GetCotacaoAsync(cotacaoRequest);
        }
    }
}
