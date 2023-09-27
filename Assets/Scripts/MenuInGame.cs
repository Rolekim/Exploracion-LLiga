using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    bool InMenu;

    [SerializeField]
    GameObject menuGO;
    [SerializeField]
    GameObject controlsGO;
    [SerializeField]
    GameObject tip1GO;
    [SerializeField]
    GameObject tip2GO;
    [SerializeField]
    GameObject tip3GO;
    [SerializeField]
    GameObject tip4GO;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SceneSave.sceneSave.inDialogue)
            {
                if (!InMenu)
                {
                    InMenu = true;
                    SceneSave.sceneSave.ChangeCanMove(false);
                    ShowMenu();

                }
                else
                {
                    SceneSave.sceneSave.ChangeCanMove(true);
                    SetAllActiveFalse();
                    InMenu = false;
                }
            }

        }
    }

    public void ShowMenu()
    {
        SoundManager.soundManager.PlayButton();
        SetAllActiveFalse();
        menuGO.SetActive(true);
    }

    public void ShowControls()
    {
        SoundManager.soundManager.PlayButton();
        SetAllActiveFalse();
        controlsGO.SetActive(true);
    }

    public void ShowTip1()
    {
        SoundManager.soundManager.PlayButton();
        SetAllActiveFalse();
        tip1GO.SetActive(true);
    }

    public void ShowTip2()
    {
        SoundManager.soundManager.PlayButton();
        SetAllActiveFalse();
        tip2GO.SetActive(true);
    }

    public void ShowTip3()
    {
        SoundManager.soundManager.PlayButton();
        SetAllActiveFalse();
        tip3GO.SetActive(true);
    }

    public void ShowTip4()
    {
        SoundManager.soundManager.PlayButton();
        SetAllActiveFalse();
        tip4GO.SetActive(true);
    }

    public void SetAllActiveFalse()
    {
        menuGO.SetActive(false);
        controlsGO.SetActive(false);
        tip1GO.SetActive(false);
        tip2GO.SetActive(false);
        tip3GO.SetActive(false);
        tip4GO.SetActive(false);
    }

    public void ToMainMenu()
    {
        SoundManager.soundManager.PlayButton();
        StartCoroutine(ToMenuCoroutine());
    }

    IEnumerator ToMenuCoroutine()
    {
        FindObjectOfType<CharacterMovement>().ReturnPositions();

        if (SceneSave.sceneSave != null)
        {
            SceneSave.sceneSave.ShowToMap();
        }
        SoundManager.soundManager.StopExplorationMusic();
        yield return new WaitForSeconds(0.5f);
        SoundManager.soundManager.PlayExplorationMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

    }
}
