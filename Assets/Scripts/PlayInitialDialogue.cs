using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInitialDialogue : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(PlayDialogue());
    }

    IEnumerator PlayDialogue()
    {
        yield return new WaitForSeconds(0.6f);

        if (SceneSave.sceneSave.firstDialogue)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();

            SceneSave.sceneSave.firstDialogue = false;
        }
    }
}
