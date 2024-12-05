using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{
    [RequireComponent(typeof(AudioSource))]
    public class TextToSpeech : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Dropdown voiceDropdown;
        [SerializeField] private Dropdown modelDropdown;
        [SerializeField] private Button button;
        [SerializeField] private Text buttonText;

        private AudioSource audioSource;
        
        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            button.onClick.AddListener(SendRequest);
        }

        private async void SendRequest()
        {
            var request = new CreateTextToSpeechRequest
            {
                Input = inputField.text,
                Model = modelDropdown.options[modelDropdown.value].text.ToLower(),
                Voice = voiceDropdown.options[voiceDropdown.value].text.ToLower()
            };

            buttonText.text = "Requesting...";
            button.interactable = false;
            
            var response = await openai.CreateTextToSpeech(request);
            
            buttonText.text = "Read";
            button.interactable = true;
            
            if(response.AudioClip) audioSource.PlayOneShot(response.AudioClip);
        }
    }
}
