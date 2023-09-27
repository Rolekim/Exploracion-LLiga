using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solo : AllyAbility
{
    [SerializeField]
    private int speedBuff = 3;
    [SerializeField]
    private int attackBuffPercentage = 30;
    [SerializeField]
    private int critBuffPercentage = 10;
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
        yield return new WaitForSeconds(0.5f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability5");
        yield return new WaitForSeconds(3f);
        AllPartySpeedBuff();
        yield return new WaitForSeconds(3f);
        AllPartyAttackBuff();
        yield return new WaitForSeconds(3f);
        AllPartyCritBuff();


        canvas.ChangeText("Todo el grupo gana 3 de velocidad, 10% de critico y 30% de ataque.");
        unit.SetAnimTrigger("Iddle");
        unit.ChangeAxis();
        unit.ChangeMove();
    }

    void AllPartySpeedBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();

        foreach (PartyUnit u in partyUnits)
        {
            u.ChangeSpeedBuff(speedBuff);
            u.InstantiatePopupText("+ VEL", "azul");
        }
    }

    void AllPartyAttackBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();
        var value = attackBuffPercentage;
        foreach (PartyUnit u in partyUnits)
        {
            value = (u.ReturnAttack() * attackBuffPercentage)/ 100;
            u.ChangeAttackBuff(value);
            u.InstantiatePopupText("+ ATQ", "azul");

        }
    }

    void AllPartyCritBuff()
    {
        var partyUnits = FindObjectsOfType<PartyUnit>();
        var value = critBuffPercentage;

        foreach (PartyUnit u in partyUnits)
        {
            value = (u.ReturnCrit() * critBuffPercentage) / 100;
            u.ChangeCritBuff(value);
            u.InstantiatePopupText("+ CRI", "azul");

        }
    }
}
