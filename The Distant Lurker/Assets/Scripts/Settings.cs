using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    //volume
    private float currentVolume;
    

    // Start is called before the first frame update
    void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("volume", 0f);
        audioMixer.SetFloat("volume", currentVolume);
        volumeSlider.value = currentVolume;
    }
    

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    

    public void SetVolume(float volume)
    {
        currentVolume = volume;
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", currentVolume);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
