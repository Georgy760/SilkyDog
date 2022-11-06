using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private SceneLoader scene_loader;

    public void OnConnectClick()
    {

    }

    public void OnPlayClick()
    {
        //sync loading scene
        //SceneManager.LoadScene("GameScene");

        //async loading scene
        scene_loader.LoadScene();
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
