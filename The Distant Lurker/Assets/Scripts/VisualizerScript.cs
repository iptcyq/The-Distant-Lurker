using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VisualizerScript : MonoBehaviour
{
    //visualiser
    private AudioManager audioManager;
    private BugManager bugManager;

    public GameObject[] visualizerObjects;
    public float sensitivity = 0.5f;

    public float minHeight;
    public float maxHeight;

    [Range(64,8192)]
    public int visualizerSimples = 64;

    public string name; //audioname

    // Start is called before the first frame update
    void Start()
    {
        bugManager = FindObjectOfType<BugManager>();
        audioManager = FindObjectOfType<AudioManager>();

        name = bugManager.audioName;
        audioManager.Play(name);
    }

    private void OnEnable()
    {
        name = bugManager.audioName;

        audioManager.Play(name);
    }

    private void OnDisable()
    {
        audioManager.Stop(name);
    }

    private void FixedUpdate()
    {
        Sound s = Array.Find(audioManager.sounds, sound => sound.name == name);
        float[] spectrumData = s.source.GetSpectrumData(visualizerSimples, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;

            newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5.0f), sensitivity * 0.5f), minHeight, maxHeight);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
            
        }
    }
}
