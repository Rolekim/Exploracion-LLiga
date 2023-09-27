using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoAlEstomago : AllyAbility
{
    public override void Act()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowChooseEnemy();
        unit.ChangeAbilitySelected(0);
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
        yield return new WaitForSeconds(0.5f);
        //ChangeOtherToDamaged(attackPoint);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability1");
        yield return new WaitForSeconds(0.6f);
        AttackUnit(attackPoint, 0);


        yield return new WaitForSeconds(1f);
        unit.UpdateSpecialCharge();
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }
}
