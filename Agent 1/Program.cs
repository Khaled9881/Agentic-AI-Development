using Google.Cloud.AIPlatform.V1;
using Google.Protobuf.WellKnownTypes;

var projectId = "agentic-dev-499117";
var location = "us-central1";
var model = "gemini-2.5-flash"; // ✅ valid current model

// Use PredictionServiceClient pointed at the correct regional endpoint
var client = new PredictionServiceClientBuilder
{
    Endpoint = $"{location}-aiplatform.googleapis.com"
}.Build();

var endpoint = $"projects/{projectId}/locations/{location}/publishers/google/models/{model}";


while (true)
{

    string prompt = Console.ReadLine();
    // ✅ Gemini requires GenerateContentAsync, NOT PredictAsync
    var request = new GenerateContentRequest
    {
        Model = endpoint,
        SystemInstruction = new Content
        {
            Parts =
            {
                new Part { Text = "You are a helpful agent. Always respond concisely." }
            }
        },
        Contents =
        {
            new Content
            {
                Role = "user",
                Parts =
                {
                    new Part { Text = prompt }
                }
            }
        }
    };

    var response = await client.GenerateContentAsync(request);

    // Print the text from the first candidate
    //Console.WriteLine(response);
    Console.WriteLine(response.Candidates[0].Content.Parts[0].Text);

}


