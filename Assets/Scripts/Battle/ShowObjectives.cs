using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectives : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObjectives;
    [SerializeField]
    private GameObject allyObjectives;

    public void ShowAllyObjectives()
    {
        allyObjectives.SetActive(true);
    }

    public void ShowEnemyObjectives()
    {
        enemyObjectives.SetActive(true);
    }

    public void HideObjectives()
    {
        allyObjectives.SetActive(false);
        enemyObjectives.SetActive(false);
    }
}
