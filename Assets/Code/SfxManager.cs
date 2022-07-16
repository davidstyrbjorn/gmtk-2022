using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedSfx
{
    public string name;
    public AudioClip clip;
}

public class SfxManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> sfxMap = new Dictionary<string, AudioClip>();
    public NamedSfx[] soundEffects;

    void Start()
    {
        // Convert soundEffects into our map
        foreach (var sfx in soundEffects)
        {
            sfxMap.Add(sfx.name, sfx.clip);
        }
    }

    public void PlaySound(string name, float volume = 1.0f, float delay = 0.0f)
    {
        if (sfxMap.TryGetValue(name, out AudioClip clip))
        {
            AudioSource source = new GameObject("SFX: " + name).AddComponent<AudioSource>();
            source.clip = clip;
            source.playOnAwake = false;
            source.volume = volume;
            if (delay > 0.01f) source.PlayDelayed(delay);
            else source.Play();
            // Destroy after done playing
            Destroy(source.gameObject, source.clip.length + delay);
            return;
        }

        Debug.LogError("SfxManager: tried to play a sound that doesn't exist! NAME: " + name);
    }
}
