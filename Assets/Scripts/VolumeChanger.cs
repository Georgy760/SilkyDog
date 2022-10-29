using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;

    private float volume;

    public void OnValueChange()
    {
        volume = slider.value;
    }
}
