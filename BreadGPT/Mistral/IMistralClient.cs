using Mistral.Types;

namespace Mistral;

public interface IMistralClient
{
    Task<string> CompleteAsync(Completion completion, CancellationToken cancellationToken = default!);
}