using Azure.AI.OpenAI;
using Azure;
using System.Text;

namespace ChatTest.Models
{
    public class AIModel
    {
        private readonly IConfiguration _configuration;
        public AIModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> AskTheMachine(string modelType, string prompt)
        {
            var openAiClient = new OpenAIClient(
                new Uri(_configuration["Azure:OpenAI:Endpoint"]),
                new AzureKeyCredential(_configuration["Azure:OpenAI:ApiKey"])
            );

            string botInit;
            switch (modelType)
            {
                case AskModelType.Epi:
                    botInit = "You are a chat bot that's used on the ECDC's website. You're an expert in the field of epidemiology and you're helping with epidemiology matters. You're called EpiAI.";
                    break;

                default:
                    botInit = "You are a chat bot that's used on the ECDC's website. Despite being on the ECDC website, you're dealing with generic matters, not necessary epidemiology related. You're called GenAI.";
                    break;
            }

            ChatCompletionsOptions chatCompletionsOptions = new()
            {
                DeploymentName = _configuration["Azure:OpenAI:Model"]
            };

            chatCompletionsOptions.Messages.Add(
                new ChatRequestSystemMessage(botInit));
            chatCompletionsOptions.Messages.Add(
                new ChatRequestUserMessage(prompt));

            var chatCompletionsResponse = await openAiClient.GetChatCompletionsStreamingAsync(
                chatCompletionsOptions
            );


            StringBuilder response = new StringBuilder();

            Dictionary<int, string> toolCallIdsByIndex = new();
            Dictionary<int, string> functionNamesByIndex = new();
            Dictionary<int, StringBuilder> functionArgumentBuildersByIndex = new();
            StringBuilder contentBuilder = new();

            await foreach (StreamingChatCompletionsUpdate chatUpdate
                in chatCompletionsResponse)
            {
                if (chatUpdate.ToolCallUpdate is StreamingFunctionToolCallUpdate functionToolCallUpdate)
                {
                    if (functionToolCallUpdate.Id != null)
                    {
                        toolCallIdsByIndex[functionToolCallUpdate.ToolCallIndex] = functionToolCallUpdate.Id;
                    }
                    if (functionToolCallUpdate.Name != null)
                    {
                        functionNamesByIndex[functionToolCallUpdate.ToolCallIndex] = functionToolCallUpdate.Name;
                    }
                    if (functionToolCallUpdate.ArgumentsUpdate != null)
                    {
                        StringBuilder argumentsBuilder
                            = functionArgumentBuildersByIndex.TryGetValue(
                                functionToolCallUpdate.ToolCallIndex,
                                out StringBuilder existingBuilder) ? existingBuilder : new StringBuilder();
                        argumentsBuilder.Append(functionToolCallUpdate.ArgumentsUpdate);
                        functionArgumentBuildersByIndex[functionToolCallUpdate.ToolCallIndex] = argumentsBuilder;
                    }
                }
                if (chatUpdate.ContentUpdate != null)
                {
                    contentBuilder.Append(chatUpdate.ContentUpdate);
                }
            }

            response.Append(contentBuilder.ToString());

            return response.ToString();
        }
    }
}
