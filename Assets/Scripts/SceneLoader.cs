using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image progress_bar;
    [SerializeField] private TextMeshProUGUI progress_text;
    [SerializeField] private GameObject load_screen;

    private AsyncOperation async_operation;
    private float progress;

    public void LoadScene()
    {
        load_screen.SetActive(true);
        StartCoroutine(AsyncLoading());
    }

    private IEnumerator AsyncLoading()
    {
        yield return new WaitForSeconds(1f);
        async_operation = SceneManager.LoadSceneAsync("GameScene");

        while (!async_operation.isDone)
        {
            progress = async_operation.progress;
            progress_bar.fillAmount = progress;
            progress_text.text = (progress * 100f).ToString() + "%";
            yield return null;
        }
    }
}
