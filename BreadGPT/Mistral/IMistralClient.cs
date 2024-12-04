using Mistral.Types;

namespace Mistral;

public interface IMistralClient
{
    Task<ChatResponse> CompleteAsync(Completion completion, CancellationToken cancellationToken = default!);
}