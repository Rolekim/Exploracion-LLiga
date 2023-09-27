using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoSucio : AllyAbility
{
    [SerializeField]
    private int evasionBuff = 5;
    [SerializeField]
    private int extraDamagePercentage = -70;
    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        unit.ChangeAttackPoint(BattleSystem.battleSystem.ReturnPositionAttackEnemies()[0].position);
        unit.ChangeMove();
        while (unit.transform.position != BattleSystem.battleSystem.ReturnPositionAttackEnemies()[0].position)
        {
            yield return null;
        }
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        yield return new WaitForSeconds(0.8f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability3");
        yield return new WaitForSeconds(0.2f);
        AttackToFirstUnits(extraDamagePercentage);
        unit.ChangeEvasionBuff(evasionBuff);
        unit.InstantiatePopupText("+ EVA", "azul");
        yield return new WaitForSeconds(1f);
        unit.ChangeAxis();
        unit.ChangeMove();
        unit.UpdateSpecialCharge();
    }


}
