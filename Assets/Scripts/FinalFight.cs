using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFight : MonoBehaviour
{
    SceneManager sceneManager;
    CharacterMovement character;

    void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        character = FindObjectOfType<CharacterMovement>();
    }
    void Update()
    {
        if(SceneSave.sceneSave.finalBattle && !SceneSave.sceneSave.inDialogue)
        {
            StartCoroutine(character.ChangeToBossBattle());
        }
    }
}
