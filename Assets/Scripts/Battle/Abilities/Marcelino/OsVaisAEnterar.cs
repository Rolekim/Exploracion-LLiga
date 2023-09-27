using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsVaisAEnterar : EnemyAbility
{
    [SerializeField]
    private int attackPercentageBuff = 20;
    public override void Act()
    {

        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        var attackValue = (unit.ReturnAttack() * attackPercentageBuff) / 100;
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        yield return new WaitForSeconds(0.6f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability4");
        yield return new WaitForSeconds(1f);
        unit.ChangeAttackBuff(attackValue);
        canvas.ChangeText("El ataque de Marcelino sube en " + attackValue + ".");
        unit.InstantiatePopupText("+ ATQ", "naranja");
    }
}
