using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aperitivo : AllyAbility
{
    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }
    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        
        yield return new WaitForSeconds(0.5f);
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());

        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability2");
        yield return new WaitForSeconds(0.3f);
        unit.Heal(5);

        
        
        yield return new WaitForSeconds(3f);
        canvas.ChangeText("Conglo se curo 5 puntos de vida.");
        unit.UpdateSpecialCharge();
    }
}
