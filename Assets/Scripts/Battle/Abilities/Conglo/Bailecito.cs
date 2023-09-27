using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bailecito : AllyAbility
{
    [SerializeField]
    private int evasionBuff = 10;
    public override void Act()
    {

        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.1f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability3");
        yield return new WaitForSeconds(2f);
        
        AllPartyEvasionBuff();
        canvas.ChangeText("Todo el grupo gana 10 puntos de evasion.");
        yield return new WaitForSeconds(0.5f);
        unit.UpdateSpecialCharge();
    }

    void AllPartyEvasionBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>(); 

        foreach(PartyUnit u in partyUnits)
        {
            u.ChangeEvasionBuff(evasionBuff);
            u.InstantiatePopupText("+ EVA", "azul");
        }
    }
}
