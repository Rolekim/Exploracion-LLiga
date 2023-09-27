using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabezazo : AllyAbility
{
    [SerializeField]
    private int extraDamagePercentage = -60;

    public override void Act()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowChooseEnemy();
        unit.ChangeAbilitySelected(1);
    }
    public override void Act2(Vector3 attackPoint, Vector3 attackPosicion)
    {
        StartCoroutine(ActCoroutine(attackPoint, attackPosicion));
    }
    IEnumerator ActCoroutine(Vector3 attackPoint, Vector3 attackPosicion)
    {
        unit.ChangeAttackPoint(attackPosicion);
        unit.ChangeMove();
        while (unit.transform.position != attackPosicion)
        {
            yield return null;
        }
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        yield return new WaitForSeconds(0.8f);
        //ChangeOtherToDamaged(attackPoint);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability2");
        yield return new WaitForSeconds(0.3f);
        AttackUnit(attackPoint, extraDamagePercentage);
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        checkPosition.CheckUnitInPosition(attackPoint).ControlUnitBuffs();
        checkPosition.CheckUnitInPosition(attackPoint).ControlUnitBuffs();

        yield return new WaitForSeconds(2.3f);
        unit.UpdateSpecialCharge();
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }

}
