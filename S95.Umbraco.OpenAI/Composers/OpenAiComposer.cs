using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using S95.Umbraco.OpenAI.Controllers;
using S95.Umbraco.OpenAI.Interfaces;
using S95.Umbraco.OpenAI.Models;
using S95.Umbraco.OpenAI.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace S95.Umbraco.OpenAI.Composers;

public class OpenAiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<UmbracoPipelineOptions>(options =>
        {
            options.AddFilter(new UmbracoPipelineFilter(nameof(OpenAiResourceController))
            {
                Endpoints = app => app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        "OpenAI Resource Controller",
                        "/App_Plugins/S95.Umbraco.OpenAI/{folder}/{file}",
                        new { Controller = "OpenAiResource", Action = "Index" }
                    );
                })
            });
        });

        builder.ManifestFilters().Append<OpenAiManifest>();

        builder.Services.Configure<Configuration>(builder.Config.GetSection("OpenAI"));
        builder.Services.AddTransient<IOpenAiService, OpenAiService>();
    }
}