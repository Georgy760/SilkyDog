using UnityEngine;

namespace Common.Scripts.Legacy
{
    public class ButtonsManager : MonoBehaviour
    {
        private SceneLoader scene_loader;

        public void OnConnectClick()
        {

        }

        private void Start()
        {
            scene_loader = AudioManager.instance.gameObject.transform.Find("ScriptHandler").gameObject.GetComponent<SceneLoader>();
        }

        public void OnPlayClick(string scene_name)
        {
            scene_loader.LoadScene(scene_name);
        }

        public void OnLeaderBoardClick()
        {

        }

        public void OnSettingsClick()
        {
        
        }

        public void OnBackClick()
        {

        }
    }
}
