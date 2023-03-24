using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MF.Example.Gateway.Handlers
{
    public class BlackListHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryGetValues("Api_Key", out var values);
            var apikey = values?.FirstOrDefault();

            if (apikey is null)
            {
                return ReturnBadRequest("Request bloqueado - Token Inválido");
            }

            if (_blackList.Any(x => x.Equals(apikey)))
            {
                return ReturnBadRequest("Request bloqueado - Usuário Bloqueado");
            }

            return base.SendAsync(request, cancellationToken);
        }

        private Task<HttpResponseMessage> ReturnBadRequest(string message) =>
            Task.Factory.StartNew(() =>
                new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message)
                });

        private List<string> _blackList => new List<string>
        {
            "123456",
            "111111"
        };
    }
}