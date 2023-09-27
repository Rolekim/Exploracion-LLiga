using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafagaDeGolpes : AllyAbility
{
    [SerializeField]
    private int extraPercentageDamage = -70;
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
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        yield return new WaitForSeconds(0.6f);
        //ChangeOtherToDamaged(attackPoint);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability2");
        yield return new WaitForSeconds(0.5f);
        AttackAllUnits();


        yield return new WaitForSeconds(1f);
        unit.UpdateSpecialCharge();
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
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
