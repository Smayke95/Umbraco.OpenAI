using Microsoft.Extensions.DependencyInjection;
using S95.Umbraco.OpenAI.Interfaces;
using S95.Umbraco.OpenAI.Models;
using S95.Umbraco.OpenAI.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace S95.Umbraco.OpenAI.Composers;

public class OpenAiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<Configuration>(builder.Config.GetSection("OpenAI"));
        builder.Services.AddTransient<IOpenAiService, OpenAiService>();
    }
}