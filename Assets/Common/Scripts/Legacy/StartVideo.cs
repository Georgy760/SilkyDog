using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Common.Scripts.Legacy
{
    public class StartVideo : MonoBehaviour
    {
        [SerializeField] private VideoPlayer video_player;
        [SerializeField] private GameObject main_panel;

        private void Awake()
        {
            video_player.url = Application.streamingAssetsPath + "/" + "Start.mp4";
            StartCoroutine(PlayVideo());
        }

        private IEnumerator PlayVideo()
        {
            video_player.Prepare();
            video_player.Play();
            yield return new WaitForSeconds(4.5f);

            main_panel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
