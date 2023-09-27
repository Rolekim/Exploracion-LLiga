using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDeSitio : AllyAbility
{
    [SerializeField]
    private int evasionBuff = 3;
    public override void Act()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowChooseAlly();
        unit.ChangeAbilitySelected(0);

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
        unit.ChangeOrderToValue(5);
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
        unit.SetAnimTrigger("Ability1");
        yield return new WaitForSeconds(3f);
        unit.UpdateSpecialCharge();
        canvas.ChangeText("La evasion de " + checkUnit.CheckUnitInPosition(attackPoint).ReturnName() + " y de Parolo ha aumentado en " + evasionBuff);
        checkUnit.CheckUnitInPosition(attackPoint).ChangeEvasionBuff(evasionBuff);
        checkUnit.CheckUnitInPosition(attackPoint).InstantiatePopupText("+ EVA", "azul");
        unit.ChangeEvasionBuff(evasionBuff);
        unit.InstantiatePopupText("+ EVA", "azul");
        checkUnit.CheckUnitInPosition(attackPoint).ChangePosition(unit.ReturnPosition());
        unit.ChangePosition(attackPoint);
        unit.SetAnimTrigger("Iddle");
        

        unit.ChangeMove();

    }
}
