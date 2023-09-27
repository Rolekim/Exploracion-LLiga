using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeVoyACastigar : AllyAbility
{
    [SerializeField]
    private int attackBuffPercentage = 60;
    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }
    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        var attackBonus = (unit.ReturnAttack() * attackBuffPercentage) / 100;
        yield return new WaitForSeconds(0.5f);
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());

        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability3");
        yield return new WaitForSeconds(1f);
        unit.ChangeAttackBuff(attackBonus);
        unit.InstantiatePopupText("+ ATQ", "azul");

        unit.Plus1TurnAttackBuff();

        yield return new WaitForSeconds(2f);
        canvas.ChangeText("Davo ha ganado un 60% mas de ataque para el siguiente turno.");
        
        unit.UpdateSpecialCharge();
    }
}
