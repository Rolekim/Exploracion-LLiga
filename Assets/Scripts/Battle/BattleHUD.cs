using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider specialAbilitySlider;
    [SerializeField]
    private UnitName unitName;

    public void SetHUD(Unit unit)
    {
        hpSlider.maxValue = unit.ReturnMaxHp();
        hpSlider.value = unit.ReturnHp();
        //specialAbilitySlider.maxValue = unit.maxSpecialCharge;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetHUDSpecialAbility(PartyUnit unit)
    {
        specialAbilitySlider.maxValue = unit.MaxSpecialCharge();
        specialAbilitySlider.value = unit.CurrentSpecialCharge();
    }

    public UnitName ReturnUnitName()
    {
        return unitName;
    }

    public void SetAbilityCharge(int charge, int maxCharge)
    {
        specialAbilitySlider.value = charge;
        if (specialAbilitySlider.value > maxCharge)
        {
            specialAbilitySlider.value = maxCharge;
        }
    }

}
