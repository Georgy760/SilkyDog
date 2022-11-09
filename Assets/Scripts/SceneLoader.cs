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

    public void LoadScene(string name)
    {
        Time.timeScale = 1;
        StartCoroutine(AsyncLoading(name));
    }

    private IEnumerator AsyncLoading(string name)
    {
        GameObject screen = Instantiate(load_screen, GameObject.FindObjectsOfType<Canvas>()[0].transform);
        progress_bar = screen.transform.Find("ProgressBar").Find("Fill").gameObject.GetComponent<Image>();
        progress_text = screen.transform.Find("ProgressBar").Find("ProgressText").gameObject.GetComponent<TextMeshProUGUI>();

        yield return new WaitForSeconds(1f);
        async_operation = SceneManager.LoadSceneAsync(name);

        while (!async_operation.isDone)
        {
            progress = async_operation.progress;
            progress_bar.fillAmount = progress;
            progress_text.text = (progress * 100f).ToString() + "%";
            yield return null;
        }
    }
}
