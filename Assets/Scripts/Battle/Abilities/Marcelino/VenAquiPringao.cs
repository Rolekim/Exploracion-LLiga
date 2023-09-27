using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenAquiPringao : EnemyAbility
{
    [SerializeField]
    private GameObject enemySummoned;
    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }

    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        unit.ChangeAttackPoint(BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position);
        unit.ChangeMove();
        while (unit.transform.position != BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        //ChangeOtherToDamaged(attackPoint);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability2");
        yield return new WaitForSeconds(2f);
        BattleSystem.battleSystem.AddEnemyUnit(enemySummoned);
        MoveMobToPos1();
        canvas.ChangeText("Marcelino ha llamado a su amigo." );
        MoveCongloToPos1();
        yield return new WaitForSeconds(2f);
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }

    void MoveCongloToPos1()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();
        PartyUnit congloUnit;
        foreach(PartyUnit u in partyUnits)
        {
            if(u.ReturnName() == "Conglo")
            {
                congloUnit = u;
                MoveUnits(congloUnit);
            }
        }

        
    }

    void MoveUnits(PartyUnit congloUnit)
    {
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2 = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3 = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4 = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;

        if (congloUnit.ReturnPosition() == new Vector2(pos2.x,pos2.y))
        {
            checkUnit.CheckUnitInPosition(pos1).ChangePosition(pos2);
        }
        else if(congloUnit.ReturnPosition() == new Vector2(pos3.x, pos3.y))
        {
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos3);
            checkUnit.CheckUnitInPosition(pos1).ChangePosition(pos2);
        }
        else if (congloUnit.ReturnPosition() == new Vector2(pos4.x, pos4.y))
        {
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos4);
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos3);
            checkUnit.CheckUnitInPosition(pos1).ChangePosition(pos2);
        }
        congloUnit.ChangePosition(pos1);
    }

    void MoveMobToPos1()
    {
        var enemies= FindObjectsOfType<EnemyUnit>();
        var pos1 = BattleSystem.battleSystem.ReturnEnemyPositions()[0].position;
        foreach (EnemyUnit e in enemies)
        {
            if(e.ReturnId() == "Maton")
            {
                unit.ChangePosition(e.ReturnPosition());
                e.ChangePosition(pos1);
            }
        }
    }
}
