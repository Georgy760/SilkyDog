using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Common.Scripts.Legacy
{
    public enum SoundType
    {
        Music,
        Effects
    }

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] internal AudioSource music_source;
        [SerializeField] internal AudioSource effects_source;
        [SerializeField] private Sound[] sounds;

        [SerializeField] private bool first_enter = true;
        private GameObject main_canvas;

        [System.Serializable]
        public class Sound
        {
            public string key;
            public AudioClip audio_clip;
        }

        public static AudioManager instance;

        private List<Button> buttons = new List<Button>();

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            buttons = new List<Button>();
            buttons.AddRange(Resources.FindObjectsOfTypeAll<Button>());

            if (buttons.Count != 0)
            {
                foreach (Button button in buttons)
                {
                    button.onClick.AddListener(OnClick);
                }
            }
        }

        private void OnDisable()
        {
            if (buttons.Count != 0)
            {
                foreach (Button button in buttons)
                {
                    button.onClick.RemoveListener(OnClick);
                }
            }
        }

        private void Awake()
        {
            AudioManager[] instances = FindObjectsOfType<AudioManager>();
            int count = instances.Length;
            if (instance)
            {
                DestroyImmediate(this.gameObject);
                return;
            }
            else if (count > 0)
            {
                if (count == 1)
                    instance = instances[0];
                for (int i = 1; i < instances.Length; i++)
                    Destroy(instances[i]);
                instance = instances[0];
            }
            instance = this;


            if (first_enter) {

            }

            DontDestroyOnLoad(this.gameObject);
        }

        public void PlayOneShot(AudioClip clip, SoundType type)
        {
            switch (type)
            {
                case SoundType.Music:
                    music_source.PlayOneShot(clip);
                    break;
                case SoundType.Effects:
                    effects_source.PlayOneShot(clip);
                    break;
            }
        }

        public void Stop(SoundType type)
        {
            switch (type)
            {
                case SoundType.Music:
                    music_source.Stop();
                    break;
                case SoundType.Effects:
                    effects_source.Stop();
                    break;
            }
        }

        public void SetVolume(float volume, SoundType type)
        {
            switch (type)
            {
                case SoundType.Music:
                    music_source.volume = volume;
                    break;
                case SoundType.Effects:
                    effects_source.volume = volume;
                    break;
            }
        }

        public AudioClip GetSound(string key)
        {
            foreach(Sound sound in sounds)
            {
                if(key == sound.key)
                {
                    return sound.audio_clip;
                }
            }

            Debug.LogError("Sound with key '" + key + "' not found");
            return null;
        }

        private void OnClick() => PlayOneShot(GetSound("button_click"), SoundType.Effects);

        private void checkOver()
        {
            VideoPlayer VP = GameObject.FindObjectsOfType<VideoPlayer>()[0];
            long playerCurrentFrame = VP.GetComponent<VideoPlayer>().frame;
            long playerFrameCount = Convert.ToInt64(VP.GetComponent<VideoPlayer>().frameCount)-4;
            if (playerCurrentFrame >= playerFrameCount) {
                main_canvas.SetActive(true);
                Destroy(VP.gameObject);
                CancelInvoke("checkOver");
            }
        }
    }
}