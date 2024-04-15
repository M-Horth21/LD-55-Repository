using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;

    Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(SliderChanged);
    }

    void OnEnable()
    {
        _audioMixer.GetFloat("MasterVolume", out var currVolume);

        _slider.value = AudioLevelFunctions.DecibelsToPercent(currVolume);
    }

    void SliderChanged(float value)
    {
        AudioManager.instance.SFXVolume(value);
        AudioManager.instance.MusicVolume(value);
        _audioMixer.SetFloat("MasterVolume",
          AudioLevelFunctions.PercentToDecibels(value));
    }
}