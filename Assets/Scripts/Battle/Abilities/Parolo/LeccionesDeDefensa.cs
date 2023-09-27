using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeccionesDeDefensa : AllyAbility
{
    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        unit.ChangeCurrentCharge(0);
        unit.UpdateSpecialCharge();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        unit.ChangeAttackPoint(BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position);
        unit.ChangeMove();
        while (unit.transform.position != BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position)
        {
            yield return null;
        }
        unit.ChangeAxis();
        yield return new WaitForSeconds(0.6f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability5");
        yield return new WaitForSeconds(3f);
        AllPartyProtectionBuff();
        canvas.ChangeText("Todo el grupo gana 1 bloqueo.");
        yield return new WaitForSeconds(1.3f);
        unit.SetAnimTrigger("Iddle");

        unit.ChangeMove();

    }

    void AllPartyProtectionBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();

        foreach (PartyUnit u in partyUnits)
        {
            u.ChangeProtectionBuff();
           u.InstantiatePopupText("PROTECCION", "azul");
        }
    }

}
