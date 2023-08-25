using Microsoft.AspNetCore.Mvc;
using S95.Umbraco.OpenAI.Interfaces;
using S95.Umbraco.OpenAI.Models;
using System.Threading.Tasks;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace S95.Umbraco.OpenAI.Controllers;

public class OpenAiController : UmbracoAuthorizedApiController
{
    private readonly IOpenAiService _openAiService;

    public OpenAiController(IOpenAiService openAiService)
    {
        _openAiService = openAiService;
    }

    [HttpPost]
    public async Task<string> Generate([FromBody] GenerateRequest request)
        => await _openAiService.Generate(request);
}