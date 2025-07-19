
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;

// Carica variabili d'ambiente dal file .env
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: true)
    .AddIniFile(".env", optional: true)
    .Build();

var openAiKey = config["OPENAI_API_KEY"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
var openAiOrg = config["OPENAI_ORG_ID"] ?? Environment.GetEnvironmentVariable("OPENAI_ORG_ID");

// Configura Semantic Kernel con OpenAI

var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        "gpt-3.5-turbo",
        openAiKey,
        openAiOrg
    )
    .Build();

Console.WriteLine("Prompt: Raccontami una curiosità sull'intelligenza artificiale.");
var prompt = "Raccontami una curiosità sull'intelligenza artificiale.";
var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine($"Risposta: {result.GetValue<string>() ?? "Nessuna risposta generata."}");
