using S95.Umbraco.OpenAI.Models;
using System.Threading.Tasks;

namespace S95.Umbraco.OpenAI.Interfaces;

public interface IOpenAiService
{
    Task<string> Generate(GenerateRequest request);
}