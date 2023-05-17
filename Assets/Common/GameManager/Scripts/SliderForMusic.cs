using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Common.GameManager.Scripts
{
    public class SliderForMusic : MonoBehaviour
    {

        [SerializeField] AudioMixer _audioMixer;
        [SerializeField] Slider _slider;
        [SerializeField] SliderType type;
        private void Awake()
        {
            _slider.onValueChanged.AddListener(ChangeValue);
        }
        private void ChangeValue(float value)
        {
            switch (type)
            {
                case SliderType.Master:
                    _audioMixer.SetFloat("MasterVolume", value);
                    break;
                case SliderType.Musics:
                    _audioMixer.SetFloat("MusicVolume", value); 
                    break;
                case SliderType.Sounds:
                    _audioMixer.SetFloat("JumpVolume", value);
                    _audioMixer.SetFloat("CoinVolume", value);
                    break;

            }
        }

    }
}
