using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextState : MonoBehaviour
{
    void Update()
    {

        BattleSystem bs = FindObjectOfType<BattleSystem>();

        if(SceneSave.sceneSave != null)
        {
            if (!SceneSave.sceneSave.inDialogue)
            {
                if (bs.ReturnNextDialogue())
                {
                    if (Input.anyKeyDown)
                    {
                        BattleSystem.battleSystem.ResizeUnitArray();
                        bs.NextTurn();
                        bs.ChangeNextDialogue(false);
                    }
                }
            }
        }
        else
        {
            if (bs.ReturnNextDialogue())
            {
                if (Input.anyKeyDown)
                {
                    BattleSystem.battleSystem.ResizeUnitArray();
                    bs.NextTurn();
                    bs.ChangeNextDialogue(false);
                }
            }
        }
    }
}
