using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUnit : Unit
{
    [SerializeField]
    private int currentSpecialCharge = 0;
    [SerializeField]
    private int initialSpecialCharge = 0;
    [SerializeField]
    private int maxSpecialCharge = 6;

    void Start()
    {
        ReturnBattleHUD().SetHUDSpecialAbility(this);
    }
    public override void Turn()
    {
        ChangeTurnMark(true);
        var dropDown = GameObject.FindGameObjectsWithTag("AbilityDropdown");
        foreach(GameObject go in dropDown)
        {
            go.SetActive(false);
        }

        ControlUnitBuffs();
        ChangeCurrentCharge(currentSpecialCharge + 1);
        UpdateSpecialCharge();
        var canvasController = FindObjectOfType<CanvasController>();

        canvasController.ShowSelectActionBox();

        canvasController.DesactivateAbilities(this);

        Debug.Log("Turno de " + ReturnName());
    }

    public override void UnitDeath()
    {
        BattleSystem.battleSystem.AllyUnitDead(this);
    }

    public int MaxSpecialCharge()
    {
        return maxSpecialCharge;
    }

    public int CurrentSpecialCharge()
    {
        return currentSpecialCharge;
    }

    public void ChangeCurrentCharge(int value)
    {
        currentSpecialCharge = value;

        if(currentSpecialCharge >= maxSpecialCharge)
        {
            currentSpecialCharge = maxSpecialCharge;
            SoundManager.soundManager.PlaySpecialBarFilled();
        }
    }

    public void UpdateSpecialCharge()
    {
        ReturnBattleHUD().SetAbilityCharge(currentSpecialCharge, maxSpecialCharge);
    }

}
