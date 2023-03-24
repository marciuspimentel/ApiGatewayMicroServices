using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MF.Example.Gateway.Handlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using MF.Example.Gateway.Services;
using MF.Example.Grpc.Protos;
using Microsoft.Extensions.Logging;

namespace MF.Example.Gateway
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOcelot(_configuration)
                .AddTransientDefinedAggregator<ConcatenateAggregator>()
                .AddDelegatingHandler<BlackListHandler>(true);

            services.AddScoped<GrpcService>();
            services.AddGrpcClient<CotacaoProtoService.CotacaoProtoServiceClient>(options => options.Address = new System.Uri(_configuration["GrpcSetttings:CotacaoUrl"]));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOcelot().Wait();
        }
    }
}