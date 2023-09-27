using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zancadilla : EnemyAbility
{
    [SerializeField]
    private Transform attack1Position;
    [SerializeField]
    private Transform attack1Point;
    [SerializeField]
    private Transform attack2Position;
    [SerializeField]
    private Transform attack2Point;
    [SerializeField]
    private int extraDamagePercentage;

    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }

    IEnumerator ActCoroutine()
    {
        var aPos = attack1Position;
        var aPoint = attack1Point;
        var ran = Random.Range(0f, 100f);
        var orderOriginal = unit.ReturnOrderInLayer();
        if(ran <= 50 || !CheckEnemyBehind(attack2Point.position))
        {
            aPos = attack1Position;
            aPoint = attack1Point;
        }
        else
        {
            aPos = attack2Position;
            aPoint = attack2Point;
        }

        unit.ChangeAttackPoint(aPos.position);
        unit.ChangeMove();
        unit.ChangeOrderInLayer(100);
        while (unit.transform.position != aPos.position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability3");
        yield return new WaitForSeconds(0.3f);
        AttackUnit(aPoint.position, extraDamagePercentage);
        if (CheckEnemyBehind(aPoint.position))
        {
            ChangePosition(ChoseMoveToPoint(aPoint.position), aPoint.position);
        }

        yield return new WaitForSeconds(1f);
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
        unit.ChangeOrderInLayer(orderOriginal);
    }

    bool CheckEnemyBehind(Vector3 enemyPosition)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();

        var positions = BattleSystem.battleSystem.ReturnAllyPositions();
        var enemies = FindObjectsOfType<PartyUnit>();
        if (enemies.Length == 1)
        {
            return false;
        }
        else
        {
            for (int x = 0; x < positions.Length; x++)
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
        var positions = BattleSystem.battleSystem.ReturnAllyPositions();
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
