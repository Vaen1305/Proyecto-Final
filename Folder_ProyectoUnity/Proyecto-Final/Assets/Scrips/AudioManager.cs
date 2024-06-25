using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }
    public void SetMasterVolumen(float volume)
    {
        float volumen = MasterSlider.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volumen);
    }
    public void SetMusicVolumen(float volume)
    {
        float volumen = MusicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volumen);
    }
    public void SetSFXVolumen(float volume)
    {
        float volumen = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volumen);
    }
}
