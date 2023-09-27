using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placaje : AllyAbility
{
    [SerializeField]
    private int extraDamagePercentage = +60;

    public override void Act()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowChooseEnemy();
        unit.ChangeAbilitySelected(3);
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
        unit.SetAnimTrigger("Ability4");
        ChangePosition(ChoseMoveToPoint(), ChoseMiddlePoint());
        yield return new WaitForSeconds(0.3f);
        AttackUnit(attackPoint, extraDamagePercentage);

        yield return new WaitForSeconds(1f);
        unit.UpdateSpecialCharge();
        
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }

    void ChangePosition(Transform moveToPoint, Transform middlePoint)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var or = unit.ReturnOrderInLayer();
        var or2 = checkPosition.CheckUnitInPosition(middlePoint.position).ReturnOrderInLayer();

        var unitMoveTo = checkPosition.CheckUnitInPosition(moveToPoint.position);
        var unitMiddle = checkPosition.CheckUnitInPosition(middlePoint.position);

        unit.ChangeOrderInLayer(checkPosition.CheckUnitInPosition(moveToPoint.position).ReturnOrderInLayer());

        unitMoveTo.ChangePosition(middlePoint.position);      
        unitMiddle.ChangePosition(unit.ReturnPosition());
        unit.ChangePosition(moveToPoint.position);
    }

    Transform ChoseMoveToPoint()
    {
        var positions = BattleSystem.battleSystem.ReturnAllyPositions();
        for (int x = 0; x < positions.Length; x++)
        {
            if (unit.ReturnPosition() == new Vector2(positions[x].position.x, positions[x].position.y))
            {
                return positions[x - 2];
            }
        }
        return null;
    }

    Transform ChoseMiddlePoint()
    {
        var positions = BattleSystem.battleSystem.ReturnAllyPositions();
        for (int x = 0; x < positions.Length; x++)
        {
            if (unit.ReturnPosition() == new Vector2(positions[x].position.x, positions[x].position.y))
            {
                return positions[x - 1];
            }
        }
        return null;
    }
}
