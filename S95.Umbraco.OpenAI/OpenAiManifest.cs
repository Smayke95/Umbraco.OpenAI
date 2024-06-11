using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace S95.Umbraco.OpenAI;

public class OpenAiManifest : IManifestFilter
{
    public void Filter(List<PackageManifest> manifests)
    {
        manifests.Add(new PackageManifest
        {
            PackageName = "Umbraco OpenAI",
            Version = "1.0.6",
            Stylesheets = new[]
            {
                "/App_Plugins/S95.Umbraco.OpenAI/Css/openAiStyle.css"
            },
            Scripts = new[]
            {
                "/App_Plugins/S95.Umbraco.OpenAI/Scripts/openAi.service.js",
                "/App_Plugins/S95.Umbraco.OpenAI/Scripts/openAiButton.component.js",
                "/App_Plugins/S95.Umbraco.OpenAI/Scripts/openAiButton.decorator.js",
                "/App_Plugins/S95.Umbraco.OpenAI/Scripts/openAiEditor.controller.js"
            }
        });
    }
}