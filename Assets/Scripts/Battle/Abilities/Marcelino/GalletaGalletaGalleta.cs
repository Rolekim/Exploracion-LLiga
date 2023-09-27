using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalletaGalletaGalleta : EnemyAbility
{
    [SerializeField]
    private int extraPercentageDamage = -6;
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
        unit.SetAnimTrigger("Ability5");
        yield return new WaitForSeconds(4f);
        AttackAllPartyUnits(extraPercentageDamage);


        yield return new WaitForSeconds(2f);
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }

}
