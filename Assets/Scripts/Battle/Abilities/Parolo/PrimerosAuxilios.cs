using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimerosAuxilios : AllyAbility
{
    [SerializeField]
    private int minHeal = 5;
    [SerializeField]
    private int maxHeal = 10;

    public override void Act()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowChooseAlly();
        unit.ChangeAbilitySelected(3);

    }

    public override void Act2(Vector3 attackPoint, Vector3 attackPosicion)
    {
        StartCoroutine(ActCoroutine(attackPoint, attackPosicion));
    }

    // Update is called once per frame
    IEnumerator ActCoroutine(Vector3 attackPoint, Vector3 attackPosicion)
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var heal = Random.Range(minHeal, maxHeal);
        unit.ChangeAttackPoint(attackPosicion);
        unit.ChangeMove();
        while (unit.transform.position != attackPosicion)
        {
            yield return null;
        }
        unit.ChangeAxis();
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        yield return new WaitForSeconds(0.3f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability4");
        yield return new WaitForSeconds(3f);
        unit.UpdateSpecialCharge();
        checkUnit.CheckUnitInPosition(attackPoint).Heal(heal);
        unit.SetAnimTrigger("Iddle");
        canvas.ChangeText("Parolo ha curado " + heal + " puntos de vida a " + checkUnit.CheckUnitInPosition(attackPoint).ReturnName());

        unit.ChangeMove();

    }
}
