using Template;
using Microsoft.SemanticKernel;

var apiKey = "<insert-your-api-key>";

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion("gpt-4o", apiKey);
builder.AddOpenAITextEmbeddingGeneration("text-embedding-3-small", apiKey);

var kernel = builder.Build();

var chat = new Chat(kernel);

Console.WriteLine("Enter your message (or 'exit' to quit):");
while (Console.ReadLine() is { } input)
{
    if (string.IsNullOrWhiteSpace(input)) continue;
    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

    await foreach (var part in chat.ExecuteAsync(input))
    {
        Console.Write(part);
    }
    Console.WriteLine();
}
