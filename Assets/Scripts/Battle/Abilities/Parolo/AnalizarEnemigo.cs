using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalizarEnemigo : AllyAbility
{
    [SerializeField]
    private int critBuff = 5;
    public override void Act()
    {

        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        unit.ChangeAttackPoint(BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position);
        unit.ChangeMove();
        while (unit.transform.position != BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.1f);
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability3");
        yield return new WaitForSeconds(3f);
        AllPartyCritBuff();
        
        yield return new WaitForSeconds(1.3f);
        canvas.ChangeText("Todo el grupo gana " + critBuff + " puntos de critico.");
        unit.UpdateSpecialCharge();
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
        
    }

    void AllPartyCritBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();

        foreach (PartyUnit u in partyUnits)
        {
            u.ChangeCritBuff(critBuff);
            u.InstantiatePopupText("+ CRI", "azul");

        }
    }
}
