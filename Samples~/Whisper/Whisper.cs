using OpenAI;
using UnityEngine;
using UnityEngine.UI;

namespace Samples.Whisper
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;
        
        private readonly string fileName = "output.wav";
        private readonly int duration = 5;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            dropdown.options.Add(new Dropdown.OptionData("Microphone not supported on WebGL"));
            #else
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
            #endif
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }
        
        private void StartRecording()
        {
            isRecording = true;
            recordButton.enabled = false;

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            
            #if !UNITY_WEBGL
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
            #endif
        }

        private async void EndRecording()
        {
            message.text = "Transcripting...";
            
            #if !UNITY_WEBGL
            Microphone.End(null);
            #endif
            
            byte[] data = SaveWav.Save(fileName, clip);
            
            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                // File = Application.persistentDataPath + "/" + fileName,
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);

            progressBar.fillAmount = 0;
            message.text = res.Text;
            recordButton.enabled = true;
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;
                progressBar.fillAmount = time / duration;
                
                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }
    }
}
