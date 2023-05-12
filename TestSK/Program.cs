using Microsoft.SemanticKernel;

var kernel = Kernel.Builder.Build();
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty;

// Asert key not empty
if (string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Please set OPENAI_API_KEY environment variable.");
    return;
}

// Azure OpenAI
kernel.Config.AddAzureTextCompletionService(
    "text-davinci-003",                  // Azure OpenAI Deployment Name
    "https://sotels-openai.openai.azure.com/", // Azure OpenAI Endpoint
    apiKey       // Azure OpenAI Key
);

// Alternative using OpenAI
// kernel.Config.AddOpenAITextCompletionService("davinci-openai",
//     "text-davinci-003",               // OpenAI Model name
//     "...your OpenAI API Key..."       // OpenAI API Key
// );

//var prompt = @"{{$input}}

//One line TLDR with the fewest words.";

//var summarize = kernel.CreateSemanticFunction(prompt);

//string text1 = @"
//1st Law of Thermodynamics - Energy cannot be created or destroyed.
//2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
//3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";

//string text2 = @"
//1. An object at rest remains at rest, and an object in motion remains in motion at constant speed and in a straight line unless acted on by an unbalanced force.
//2. The acceleration of an object depends on the mass of the object and the amount of force applied.
//3. Whenever one object exerts a force on another object, the second object exerts an equal and opposite on the first.";

//Console.WriteLine(await summarize.InvokeAsync(text1));

//Console.WriteLine(await summarize.InvokeAsync(text2));

string translationPrompt = @"{{$input}}

Translate the text to math.";

string summarizePrompt = @"{{$input}}

Give me a TLDR with the fewest words.";

var translator = kernel.CreateSemanticFunction(translationPrompt);
var summarize = kernel.CreateSemanticFunction(summarizePrompt);

string inputText = @"
1st Law of Thermodynamics - Energy cannot be created or destroyed.
2nd Law of Thermodynamics - For a spontaneous process, the entropy of the universe increases.
3rd Law of Thermodynamics - A perfect crystal at zero Kelvin has zero entropy.";

// Run two prompts in sequence (prompt chaining)
var output = await kernel.RunAsync(inputText, translator, summarize);

Console.WriteLine(output);

// Output: ΔE = 0, ΔSuniv > 0, S = 0 at 0K.