using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpenAI
{
    public class TextCompletionChat : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform context;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";
        private string userInput;
        
        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }
        
        private void AppendMessage(string message, bool isUser = true)
        {
            context.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(isUser ? sent : received, context);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
            item.anchoredPosition = new Vector2(0, -height);
            height += item.sizeDelta.y;
            context.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        private async void SendReply()
        {
            userInput = inputField.text;
            prompt += $"{userInput}\nA: ";
            AppendMessage(userInput);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = prompt,
                Model = "text-davinci-003",
                MaxTokens = 128
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                AppendMessage(completionResponse.Choices[0].Text, false);
                prompt += $"{completionResponse.Choices[0].Text}\nQ: ";
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
