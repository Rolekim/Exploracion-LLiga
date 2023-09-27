using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject AbilityDropDown;
    [SerializeField]
    private DropDownAbilities ability;
    bool activated = false;
    [SerializeField]
    private int abilityNumber;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (activated)
            {
                var otherActivated = FindObjectsOfType<RightClickButton>();
                foreach (RightClickButton a in otherActivated)
                {
                    a.activated = false;

                }
                activated = false;
                AbilityDropDown.SetActive(false);

            }
            else
            {
                Debug.Log("Right Mouse Button Clicked on: " + name);
                var allAbilityDropDowns = GameObject.FindGameObjectsWithTag("AbilityDropdown");
                foreach (GameObject a in allAbilityDropDowns)
                {
                    a.SetActive(false);

                }
                var otherActivated = FindObjectsOfType<RightClickButton>();
                foreach (RightClickButton a in otherActivated)
                {
                    a.activated = false;

                }

                ability.SetNameAndDescription(BattleSystem.battleSystem.ReturnUnitTurn().
                abilities[abilityNumber].GetComponent<AllyAbility>().ReturnAbilityName(),
                BattleSystem.battleSystem.ReturnUnitTurn().abilities[abilityNumber].GetComponent<AllyAbility>()
                .ReturnAbilityDescription(), BattleSystem.battleSystem.ReturnUnitTurn().abilities[abilityNumber].GetComponent<AllyAbility>().ReturnPositions());
                AbilityDropDown.SetActive(true);
                activated = true;
            }

        }
    }

}
