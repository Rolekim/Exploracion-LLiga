using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeRun : EnemyAbility
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
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var orderOriginal = unit.ReturnOrderInLayer();
        unit.ChangeAttackPoint(attackPosition.position);
        unit.ChangeMove();
        unit.ChangeOrderInLayer(100);
        while (unit.transform.position != attackPosition.position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability2");
        yield return new WaitForSeconds(0.3f);
        AttackUnit(attackPoint.position, extraDamagePercentage);
        MoveUnits(checkUnit.CheckUnitInPosition(BattleSystem.battleSystem.ReturnAllyPositions()[0].position));
        //ChangeOtherToDamaged(attackPoint.position);
        yield return new WaitForSeconds(1f);
        unit.SetAnimTrigger("Iddle");
        checkUnit.CheckUnitInPosition(BattleSystem.battleSystem.ReturnAllyPositions()[3].position).ResetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
        unit.ChangeOrderInLayer(orderOriginal);
    }

    void MoveUnits(Unit u)
    {
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2 = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3 = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4 = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;

        if (!u.ReturnIsDead())
        {
            if (checkUnit.CheckUnitInPosition(pos2) != null)
            {
                checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos1);
            }
            if (checkUnit.CheckUnitInPosition(pos3) != null)
            {
                checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            }
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }

            if (checkUnit.CheckUnitInPosition(pos2) == null)
            {
                u.ChangePosition(pos2);
            }
            else if(checkUnit.CheckUnitInPosition(pos3) == null)
            {
                u.ChangePosition(pos3);

            }
            else
            {
                u.ChangePosition(pos4);
            }

        }
        
    }
}
