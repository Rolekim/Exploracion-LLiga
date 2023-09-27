using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void ChangeSceneToBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }

    public void ChangeSceneToBoss()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BossBattleScene");
    }

}
