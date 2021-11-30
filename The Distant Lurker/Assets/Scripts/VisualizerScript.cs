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
    
    private float[] spectrumData = new float[128];

    public string name; //audioname

    private void Awake()
    {
        bugManager = FindObjectOfType<BugManager>();
        audioManager = FindObjectOfType<AudioManager>();

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
        //s.source.GetOutputData(spectrumData, 0);
        s.source.GetSpectrumData(spectrumData, 0,FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;
            
            newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5.0f), sensitivity * 0.5f), minHeight, maxHeight);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
            

        }
    }
}
