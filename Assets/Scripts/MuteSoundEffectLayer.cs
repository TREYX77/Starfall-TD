using UnityEngine;

public class MuteSoundEffectLayer : MonoBehaviour
{
    [SerializeField] private string soundEffectLayerName = "SoundEffect";

    public void MuteLayerAudio()
    {
        int layer = LayerMask.NameToLayer(soundEffectLayerName);
        if (layer == -1)
        {
            Debug.LogError($"Layer '{soundEffectLayerName}' not found.");
            return;
        }

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio.gameObject.layer == layer)
            {
                audio.mute = true;
            }
        }
    }
}