using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeDestrozaPulmones : EnemyAbility
{
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private int extraDamagePercentage;

    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }

    IEnumerator ActCoroutine()
    {
        var orderOriginal = unit.ReturnOrderInLayer();
        unit.ChangeAttackPoint(attackPosition.position);
        unit.ChangeMove();
        unit.ChangeOrderInLayer(100);
        while(unit.transform.position != attackPosition.position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability1");
        yield return new WaitForSeconds(0.3f);
        AttackUnit(attackPoint.position, extraDamagePercentage);
        //ChangeOtherToDamaged(attackPoint.position);
        yield return new WaitForSeconds(1f);
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
        unit.ChangeOrderInLayer(orderOriginal);
    }

}
