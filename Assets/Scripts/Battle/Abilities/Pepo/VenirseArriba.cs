using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenirseArriba : AllyAbility
{
    [SerializeField]
    private int defensePercentageBuff = 20;
    public override void Act()
    {

        StartCoroutine(ActCoroutine());
    }

    // Update is called once per frame
    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        GameObject qteGO = Instantiate(unit.ReturnQteGo(), BattleSystem.battleSystem.ReturnQtePos());
        yield return new WaitForSeconds(0.6f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability1");
        yield return new WaitForSeconds(0.5f);
        unit.ChangeDefenseBuff(defensePercentageBuff);
        unit.InstantiatePopupText("+ DEF", "azul");
        yield return new WaitForSeconds(0.5f);
        canvas.ChangeText("Pepo recibe un 20% menos de daño.");
        unit.UpdateSpecialCharge();
    }
}
