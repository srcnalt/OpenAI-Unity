using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;

namespace OpenAI
{
    public class StreamResponse : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        
        private OpenAIApi openai = new OpenAIApi();
        private CancellationTokenSource token = new CancellationTokenSource();
        
        private void Start()
        {
            button.onClick.AddListener(SendMessage);
        }
        
        private void SendMessage()
        {
            button.enabled = false;

            var message = new List<ChatMessage>
            {
                new ChatMessage()
                {
                    Role = "user",
                    Content = "Write a 100 word long short story in La Fontaine style."
                }
            };
            
            openai.CreateChatCompletionAsync(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = message,
                Stream = true
            }, HandleResponse, null, token);

            button.enabled = true;
        }

        private void HandleResponse(List<CreateChatCompletionResponse> responses)
        {
            text.text = string.Join("", responses.Select(r => r.Choices[0].Delta.Content));
        }

        private void OnDestroy()
        {
            token.Cancel();
        }
    }
}
