using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    Animator TitleAnim;

    public void ShowSettings()
    {
        SoundManager.soundManager.PlayButton();
        TitleAnim.SetBool("Settings", true);
    }

    public void DontShowSettings()
    {
        SoundManager.soundManager.PlayButton();
        TitleAnim.SetBool("Settings", false);
    }

    public void ShowCredits()
    {
        SoundManager.soundManager.PlayButton();
        TitleAnim.SetBool("Credits", true);
    }

    public void DontShowCredits()
    {
        SoundManager.soundManager.PlayButton();
        TitleAnim.SetBool("Credits", false);
    }

    public void StartGame()
    {
        SoundManager.soundManager.PlayButton();
        TitleAnim.SetTrigger("Start");
        StartCoroutine(ToMapCoroutine());
    }

    IEnumerator ToMapCoroutine()
    {
        yield return new WaitForSeconds(1f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("ExplorationScene");
    }


    public void ExitGame()
    {
        Application.Quit();
    }

}
