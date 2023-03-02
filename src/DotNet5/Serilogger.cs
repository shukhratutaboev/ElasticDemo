using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace DotNet5
{
    public static class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

               configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUri))
                        {
                            IndexFormat = $"{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            ModifyConnectionSettings = connection => connection
                                .DisableAutomaticProxyDetection()
                                .BasicAuthentication(context.Configuration["ElasticConfiguration:Username"], context.Configuration["ElasticConfiguration:Password"]),
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        })
                    .ReadFrom.Configuration(context.Configuration);
           };
    }
}