using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string gameScene;

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void SelectStage(int stageNumber)
    {
        GameManager.instance.currentLevel = stageNumber;
        StartCoroutine(FadeScene(gameScene));
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeScene(sceneName));
    }

    public IEnumerator FadeScene(string sceneName)
    {
        Fade.instance.FadeOut();

        yield return new WaitForSeconds(0.5f);

        Fade.instance.FadeIn();
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
