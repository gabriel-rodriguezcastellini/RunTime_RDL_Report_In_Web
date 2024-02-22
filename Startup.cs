using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RunTime_RDL_Report_In_Web.Reports;
using System;
using System.Reflection;
using System.Text;

namespace RunTime_RDL_Report_In_Web
{
    public class Startup
    {
        public static string EmbeddedReportsPrefix { get; set; } = "RunTime_RDL_Report_in_Web.Reports";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _ = services
                .AddLogging(config =>
                {
                    // Disable the default logging configuration
                    _ = config.ClearProviders();

                    // Enable logging for debug mode only
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                    {
                        _ = config.AddConsole();
                    }
                })
                .AddReporting()
                .AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder =>
                    {
                        _ = builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
                })
                .AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }

            _ = app.UseReporting(settings =>
            {
                settings.UseEmbeddedTemplates(EmbeddedReportsPrefix, Assembly.GetAssembly(GetType()));
                settings.UseCustomStore(GetReport);
                settings.UseCompression = true;
            });

            _ = app.UseMvc();
        }

        private object GetReport(string arg)
        {
            if (!IsReport(arg))
            {
                return null;
            }

            ReportDefinition reportDef = new();
            return reportDef.rdlReport;
        }

        private static bool IsReport(string reportId)
        {
            return reportId.Equals("MyReport", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
