using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s==null) { Debug.Log("AudioNotFound"); return; }
        s.source.Play();
    }

    //FindObjectOfType<AudioManager>().Play(audioname); - used when implmenting audio

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { Debug.Log("AudioNotFound"); return; }
        s.source.Stop();
    
    }

    private void Start()
    {
        Play("theme");
    }

    public float audioLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float length = s.source.clip.length;
        return length;
    }

    public bool audioPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        bool y = s.source.isPlaying;
        return y;

    }
}
