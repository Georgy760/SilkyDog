using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private SoundType type;

    private float volume;

    private void Awake()
    {
        slider.value = type == SoundType.Music ? AudioManager.instance.music_source.volume : AudioManager.instance.effects_source.volume;
    }

    public void OnValueChange()
    {
        volume = slider.value;
        AudioManager.instance.SetVolume(volume, type);
    }
}
