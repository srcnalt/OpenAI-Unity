using UnityEngine;

namespace OpenAI
{
    public interface IAudioResponse: IResponse
    {
        public AudioClip AudioClip { get; set; }
    }
}