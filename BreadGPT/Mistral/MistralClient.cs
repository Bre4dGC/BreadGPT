using Mistral.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;

namespace Mistral;

public class MistralClient : IMistralClient
{
    private const string BaseUrl = "https://api.mistral.ai/v1/";

    private HttpClient _client;
    private string _apiKey;

    public MistralClient(string apiKey)
    {
        _apiKey = apiKey;
        _client = new HttpClient() { BaseAddress = new Uri(BaseUrl), };
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> CompleteAsync(Completion completion, CancellationToken cancellationToken = default!)
    {
        var settings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        };

        var contentRequest = JsonConvert.SerializeObject(completion, settings);

        var requestUrl = "chat/completions";

        var content = new StringContent(contentRequest, Encoding.UTF8, "application/json");
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl) { Content = content };

        var responseMessage = await _client.SendAsync(
            httpRequestMessage,
            cancellationToken
            );

        if (!responseMessage.IsSuccessStatusCode)
            throw new ApiResultException(responseMessage.StatusCode.ToString());

        var contentResponse = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

        var chatResponse = JsonConvert.DeserializeObject<ChatResponse>(contentResponse, settings);
        return chatResponse.Choices[0].Message.Content!;
    }
}
