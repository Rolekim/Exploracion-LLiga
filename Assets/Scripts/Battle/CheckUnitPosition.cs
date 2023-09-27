using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnitPosition : MonoBehaviour
{
    private Transform[] positionListAllies;
    private Transform[] positionListEnemies;
    private Unit[] unitsInBattlefield;
    void Start()
    {
        BattleSystem bs = GetComponent<BattleSystem>();

        positionListAllies = bs.ReturnAllyPositions();
        positionListEnemies = bs.ReturnEnemyPositions();
        unitsInBattlefield = bs.ReturnUnits();

    }

    public Unit CheckUnitInPosition(Vector2 position)
    {
        unitsInBattlefield = FindObjectsOfType<Unit>();

        foreach(Unit u in unitsInBattlefield)
        {
            if(u != null)
            {
                if (u.ReturnPosition() == position)
                {
                    return u;

                }
            }

        }
        return null;

    }

}
