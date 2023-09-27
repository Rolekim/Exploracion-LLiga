using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeDeFuego : AllyAbility
{

    [SerializeField]
    private int extraDamagePercentage = 50;

    public override void Act()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowChooseEnemy();
        unit.ChangeAbilitySelected(4);
    }
    public override void Act2(Vector3 attackPoint, Vector3 attackPosicion)
    {
        StartCoroutine(ActCoroutine(attackPoint, attackPosicion));
    }
    IEnumerator ActCoroutine(Vector3 attackPoint, Vector3 attackPosicion)
    {
        unit.ChangeCurrentCharge(0);
        unit.UpdateSpecialCharge();
        unit.ChangeAttackPoint(attackPosicion);
        unit.ChangeMove();
        while (unit.transform.position != attackPosicion)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        //ChangeOtherToDamaged(attackPoint);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability5");
        yield return new WaitForSeconds(1.1f);
        AttackUnit(attackPoint, extraDamagePercentage);

        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }
}
