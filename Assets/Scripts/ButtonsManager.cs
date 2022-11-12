using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //sync loading scene
        //SceneManager.LoadScene("GameScene");

        //async loading scene
        Time.timeScale = 1;
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
