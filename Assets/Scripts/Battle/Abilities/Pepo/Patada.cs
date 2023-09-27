using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patada : AllyAbility
{
    [SerializeField]
    private int extraDamagePercentage = -25;

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
        if (CheckEnemyBehind(attackPoint))
        {
            ChangePosition(ChoseMoveToPoint(attackPoint), attackPoint);
        }
        yield return new WaitForSeconds(0.5f);


        unit.UpdateSpecialCharge();
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();

    }

    bool CheckEnemyBehind(Vector3 enemyPosition)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();

        var positions = BattleSystem.battleSystem.ReturnEnemyPositions();
        var enemies = FindObjectsOfType<EnemyUnit>();
        if (enemies.Length <= 1)
        {
            return false;
        }
        else
        {
            for (int x = 0; x < positions.Length; x++)
            {
                if(checkPosition.CheckUnitInPosition(enemyPosition).ReturnPosition() != null)
                {
                    if (checkPosition.CheckUnitInPosition(enemyPosition).ReturnPosition() == new Vector2(positions[x].position.x, positions[x].position.y))
                    {
                        var pos = positions[x + 1];

                        for (int y = 0; y < enemies.Length; y++)
                        {
                            if (enemies[y].ReturnPosition() == new Vector2(pos.position.x, pos.position.y))
                            {
                                return true;
                            }
                        }

                    }
                }
                
            }
        }

        return false;

    }

    void ChangePosition(Transform moveToPoint, Vector3 enemyPosition)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var or = checkPosition.CheckUnitInPosition(enemyPosition).ReturnOrderInLayer();
        checkPosition.CheckUnitInPosition(enemyPosition).ChangeOrderInLayer(checkPosition.CheckUnitInPosition(moveToPoint.position).ReturnOrderInLayer());
        checkPosition.CheckUnitInPosition(moveToPoint.position).ChangeOrderInLayer(or);
        checkPosition.CheckUnitInPosition(moveToPoint.position).ChangePosition(checkPosition.CheckUnitInPosition(enemyPosition).ReturnPosition());

        checkPosition.CheckUnitInPosition(enemyPosition).ChangePosition(moveToPoint.position);
    }

    Transform ChoseMoveToPoint(Vector3 enemyPosition)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var positions = BattleSystem.battleSystem.ReturnEnemyPositions();
        for (int x = 0; x < positions.Length; x++)
        {
            if (checkPosition.CheckUnitInPosition(enemyPosition).ReturnPosition() == new Vector2(positions[x].position.x, positions[x].position.y))
            {
                return positions[x + 1];
            }
        }
        return null;
    }


}
