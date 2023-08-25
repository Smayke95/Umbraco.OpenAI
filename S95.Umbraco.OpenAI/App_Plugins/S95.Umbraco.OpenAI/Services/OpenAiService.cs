using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using S95.Umbraco.OpenAI.Interfaces;
using S95.Umbraco.OpenAI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Exceptions;

namespace S95.Umbraco.OpenAI.Services;

public class OpenAiService : IOpenAiService
{
    private readonly Configuration _openAiSettings;
    private readonly HttpClient _httpClient;

    public OpenAiService(IOptions<Configuration> openAiOptions)
    {
        _openAiSettings = openAiOptions.Value;
        _httpClient = new HttpClient();
    }

    public async Task<string> Generate(GenerateRequest request)
    {
        if (string.IsNullOrWhiteSpace(_openAiSettings.ApiKey))
            throw new ConfigurationException("ApiKey is missing in appsettings.json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _openAiSettings.ApiKey);

        var messages = new List<object>
        {
            new
            {
                role = "system",
                content = Constants.SystemMessage[(int)request.Type]
            },
            new
            {
                role = "user",
                content = request.Text
            }
        };

        var requestBody = JsonConvert.SerializeObject(new
        {
            model = string.IsNullOrWhiteSpace(_openAiSettings.Model)
                ? Constants.DefaultModel
                : _openAiSettings.Model,
            messages,
            temperature = (int)request.BehaviorModel / 2,
            max_tokens = request.MaximumLength
        });

        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(Constants.ApiUrl, content);

        if (response.StatusCode is HttpStatusCode.Unauthorized)
            throw new InvalidOperationException("ApiKey is not valid");

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Failed to generate text from OpenAI API");

        var returnedText = await GetGeneratedText(response);

        return returnedText;
    }

    private async Task<string> GetGeneratedText(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
        var returnedText = responseObject.choices[0].message.content.ToString();
        return returnedText;
    }
}