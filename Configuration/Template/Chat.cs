using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Template
{
    public class Chat
    {
        private readonly Kernel _kernel;
        public readonly AgentGroupChat _chat;

        public Chat(Kernel kernel)
        {
            _kernel = kernel;

            var agent = new ChatCompletionAgent()
            {
                Name = "",
                Instructions = "",
                Kernel = _kernel,
                //Arguments = new KernelArguments(
                //    new OpenAIPromptExecutionSettings()
                //    {
                //        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                //    }),
            };

            var selectionFunction = KernelFunctionFactory.CreateFromPrompt(
                $$$"""

                History:
                {{$History}}
                """);

            _chat = new(agent)
            {
                ExecutionSettings = new()
                {
                    SelectionStrategy = new KernelFunctionSelectionStrategy(selectionFunction, _kernel)
                    {
                        AgentsVariableName = "agents",
                        HistoryVariableName = "history",
                    }
                }
            };
        }

        public async IAsyncEnumerable<string?> ExecuteAsync(string prompt)
        {
            _chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, prompt));

            await foreach (var content in _chat.InvokeStreamingAsync())
            {
                yield return content.Content;
            }
        }
    }
}
