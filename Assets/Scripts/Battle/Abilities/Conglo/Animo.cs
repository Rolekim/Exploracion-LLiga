using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animo : AllyAbility
{
    [SerializeField]
    private int attackBuffpercentage = 20;
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
        var attackBonus = (checkUnit.CheckUnitInPosition(attackPoint).ReturnAttack() * attackBuffpercentage) / 100;
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
        checkUnit.CheckUnitInPosition(attackPoint).ChangeAttackBuff(attackBonus);
        checkUnit.CheckUnitInPosition(attackPoint).InstantiatePopupText("+ ATQ", "azul");
        unit.SetAnimTrigger("Iddle");
        canvas.ChangeText("El ataque de " + checkUnit.CheckUnitInPosition(attackPoint).ReturnName() + " ha aumentado en " + attackBonus);

        unit.ChangeMove();
        
    }
}
