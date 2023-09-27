using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodillazo : AllyAbility
{
    [SerializeField]
    private int extraDamagePercentage = -60;

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
        yield return new WaitForSeconds(0.3f);
        //ChangeOtherToDamaged(attackPoint);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability4");
        yield return new WaitForSeconds(0.6f);
        AttackUnit(attackPoint, extraDamagePercentage);
        ChangePosition(ChoseMoveToPoint());
        yield return new WaitForSeconds(1f);
        unit.SetAnimTrigger("Iddle");
        unit.UpdateSpecialCharge();
        unit.ChangeAxis();
        unit.ChangeMove();
    }

    void ChangePosition(Transform moveToPoint)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var or = unit.ReturnOrderInLayer();
        unit.ChangeOrderInLayer(checkPosition.CheckUnitInPosition(moveToPoint.position).ReturnOrderInLayer());
        checkPosition.CheckUnitInPosition(moveToPoint.position).ChangeOrderInLayer(or);
        checkPosition.CheckUnitInPosition(moveToPoint.position).ChangePosition(unit.ReturnPosition());

        unit.ChangePosition(moveToPoint.position);
    }

    Transform ChoseMoveToPoint()
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
