using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;

namespace Microsoft.AspNetCore.Builder
{
    public static class MonitoringApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMyMonitoring(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                        new
                        {
                            status = report.Status.ToString(),
                            errors = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                        }, Formatting.Indented);
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(result);
                    }
                });
            return app;
        }
    }
}