using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCamisa : AllyAbility
{
    [SerializeField]
    private int defensePercentageBuff = 20;
    [SerializeField]
    private int extraPercentageDamage = 20;

    public override void Act()
    {

        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        unit.ChangeCurrentCharge(0);
        unit.UpdateSpecialCharge();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        unit.ChangeAttackPoint(BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position);
        unit.ChangeMove();
        while (unit.transform.position != BattleSystem.battleSystem.ReturnPositionAttackEnemies()[4].position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.6f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability5");
        yield return new WaitForSeconds(1.5f);
        AttackAllUnits();
        AllPartyDefenseBuff();
        unit.SetAnimTrigger("Iddle");
        yield return new WaitForSeconds(2.5f);

        unit.ChangeAxis();
        unit.ChangeMove();

    }

    void AllPartyDefenseBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();

        foreach (PartyUnit u in partyUnits)
        {
            u.ChangeDefenseBuff(defensePercentageBuff);
            u.InstantiatePopupText("+ DEF", "azul");

        }
    }

    void AttackAllUnits()
    {

        var enemies = FindObjectsOfType<EnemyUnit>();
        Debug.Log(enemies.Length);
        foreach (EnemyUnit e in enemies)
        {
            AttackUnit(e.ReturnPosition(), extraPercentageDamage);
        }
    }
}
