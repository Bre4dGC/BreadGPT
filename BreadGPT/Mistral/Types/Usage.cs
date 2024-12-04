namespace Mistral.Types;

public class Usage
{
    public int PromptTokens { get; set; }
    public int TotalTokens { get; set; }
    public int CompletionTokens { get; set; }
}