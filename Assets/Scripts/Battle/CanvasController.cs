using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    //DialogueBox
    [SerializeField]
    private GameObject VictoryBox = null;
    [SerializeField]
    Animator victoryAnim = null;
    [SerializeField]
    private GameObject DefeatBox = null;
    [SerializeField]
    Animator defeatAnim = null;
    [SerializeField]
    private GameObject DialogueBox;
    private TextMeshPro dialogueBoxText;
    [SerializeField]
    private float writtingSpeed = 0.1f;
    [SerializeField]
    private GameObject nextArrow;

    //AbilityBox
    [SerializeField]
    private GameObject AbilityBox;

    //SelectActionBox
    [SerializeField]
    private GameObject SelectActionBox;

    //Select objectives buttons
    [SerializeField]
    private GameObject SelectAllyObjective;
    [SerializeField]
    private GameObject SelectEnemyObjective;
    [SerializeField]
    private GameObject changeAllyButtons;

    [SerializeField]
    private GameObject[] selectChangeButtons;
    [SerializeField]
    private GameObject[] selectChangeSprites;
    [SerializeField]
    private GameObject[] selectAllyButtons;
    [SerializeField]
    private GameObject[] selectAllySprites;
    [SerializeField]
    private GameObject[] selectEnemyButtons;
    [SerializeField]
    private GameObject[] selectEnemySprites;

    [SerializeField]
    private UnityEngine.UI.Button[] abilityList;



    void Awake()
    {
        dialogueBoxText = DialogueBox.GetComponentInChildren<TextMeshPro>();
        dialogueBoxText.text = "";
    }

    public void ShowDialogueBox()
    {
        SetOneActive(DialogueBox, AbilityBox, SelectActionBox, SelectAllyObjective, SelectEnemyObjective, changeAllyButtons);
    }

    public void ShowAbilityBox()
    {
        SetOneActive(AbilityBox, DialogueBox, SelectActionBox, SelectAllyObjective, SelectEnemyObjective, changeAllyButtons);
        SoundManager.soundManager.PlayButton();
    }

    public void ShowSelectActionBox()
    {
        SetOneActive(SelectActionBox, DialogueBox, AbilityBox, SelectAllyObjective, SelectEnemyObjective, changeAllyButtons);
    }

    public void ShowChooseEnemy()
    {
        Debug.Log("ShowChooseEnemy");
        SetOneActive(SelectEnemyObjective, DialogueBox, SelectActionBox, SelectAllyObjective, SelectAllyObjective, changeAllyButtons);
        DesactivateEnemyPosition();
        SoundManager.soundManager.PlayButton();
    }

    public void ShowChooseAlly()
    {
        SetOneActive(SelectAllyObjective, DialogueBox, AbilityBox, AbilityBox, SelectEnemyObjective, changeAllyButtons);
        DesactivateUnitPosition();
        SoundManager.soundManager.PlayButton();
    }

    public void ShowChooseChange()
    {
        SetOneActive(changeAllyButtons, DialogueBox, AbilityBox, AbilityBox, SelectEnemyObjective, SelectEnemyObjective);
        DesactivateUnitPosition();
        SoundManager.soundManager.PlayButton();
    }

    public void ShowVictory()
    {
        StartCoroutine(WaitVictory());
    }

    public void ShowDefeat()
    {
        StartCoroutine(WaitDefeat());
    }

    IEnumerator WaitVictory()
    {
        yield return new WaitForSeconds(1f);
        victoryAnim.SetTrigger("Show");
        SetOneActive(VictoryBox, DialogueBox, SelectAllyObjective, AbilityBox, SelectEnemyObjective, SelectActionBox);
        SoundManager.soundManager.PlayVictory();
    }

    IEnumerator WaitDefeat()
    {
        yield return new WaitForSeconds(1f);
        defeatAnim.SetTrigger("Show");
        SetOneActive(DefeatBox, DialogueBox, SelectAllyObjective, AbilityBox, SelectEnemyObjective, SelectActionBox);
        SoundManager.soundManager.PlayDefeat();
    }

    public void ChangeText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    public void CleanText()
    {
        nextArrow.SetActive(false);
        dialogueBoxText.text = "";
    }

    IEnumerator ShowText(string text)
    {
        BattleSystem bs = FindObjectOfType<BattleSystem>();
        
        

        for (int x = 0; x <= text.Length; x++)
        {
            dialogueBoxText.text = text.Substring(0, x);
            yield return new WaitForSeconds(writtingSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        bs.ChangeNextDialogue(true);
        nextArrow.SetActive(true);

        
    }

    private void SetOneActive(GameObject active, GameObject other1, GameObject other2, GameObject other3, GameObject other4, GameObject other5)
    {
        active.SetActive(true);
        other1.SetActive(false);
        other2.SetActive(false);
        other3.SetActive(false);
        other4.SetActive(false);
        other5.SetActive(false);
    }

    public void ChangeImageButtons()
    {
        for(int x = 0; x < abilityList.Length; x++)
        {
            if (BattleSystem.battleSystem.ReturnUnitTurn() != null)
            {
                if (BattleSystem.battleSystem.ReturnUnitTurn().abilities[x].ReturnSprite() != null)
                {
                    abilityList[x].image.sprite = BattleSystem.battleSystem.ReturnUnitTurn().abilities[x].ReturnSprite();
                }
            }
           
        }
    }

    public void DesactivateUnitPosition()
    {

        foreach(GameObject go in selectAllyButtons)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in selectAllySprites)
        {
            go.SetActive(true);
        }

        foreach(GameObject go in selectChangeButtons)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in selectChangeSprites)
        {
            go.SetActive(true);
        }

        Vector2 pos1 = new Vector2(BattleSystem.battleSystem.ReturnAllyPositions()[0].position.x, BattleSystem.battleSystem.ReturnAllyPositions()[0].position.y);
        Vector2 pos2 = new Vector2(BattleSystem.battleSystem.ReturnAllyPositions()[1].position.x, BattleSystem.battleSystem.ReturnAllyPositions()[1].position.y);
        Vector2 pos3 = new Vector2(BattleSystem.battleSystem.ReturnAllyPositions()[2].position.x, BattleSystem.battleSystem.ReturnAllyPositions()[2].position.y);
        Vector2 pos4 = new Vector2(BattleSystem.battleSystem.ReturnAllyPositions()[3].position.x, BattleSystem.battleSystem.ReturnAllyPositions()[3].position.y);

        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == pos1)
        {
            selectAllyButtons[0].SetActive(false);
            selectAllySprites[0].SetActive(false);
            selectChangeButtons[0].SetActive(false);
            selectChangeSprites[0].SetActive(false);
        }
        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == pos2)
        {
            selectAllyButtons[1].SetActive(false);
            selectAllySprites[1].SetActive(false);
            selectChangeButtons[1].SetActive(false);
            selectChangeSprites[1].SetActive(false);
        }
        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == pos3)
        {
            selectAllyButtons[2].SetActive(false);
            selectAllySprites[2].SetActive(false);
            selectChangeButtons[2].SetActive(false);
            selectChangeSprites[2].SetActive(false);
        }
        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == pos4)
        {
            selectAllyButtons[3].SetActive(false);
            selectAllySprites[3].SetActive(false);
            selectChangeButtons[3].SetActive(false);
            selectChangeSprites[3].SetActive(false);
        }
        var partyUnits = FindObjectsOfType<PartyUnit>();
        if(partyUnits.Length == 3)
        {
            selectAllyButtons[3].SetActive(false);
            selectAllySprites[3].SetActive(false);
            selectChangeButtons[3].SetActive(false);
            selectChangeSprites[3].SetActive(false);
        }
        else if(partyUnits.Length == 2)
        {
            selectAllyButtons[3].SetActive(false);
            selectAllySprites[3].SetActive(false);
            selectChangeButtons[3].SetActive(false);
            selectChangeSprites[3].SetActive(false);
            selectAllyButtons[2].SetActive(false);
            selectAllySprites[2].SetActive(false);
            selectChangeButtons[2].SetActive(false);
            selectChangeSprites[2].SetActive(false);
        }
        else if(partyUnits.Length == 1)
        {
            selectAllyButtons[3].SetActive(false);
            selectAllySprites[3].SetActive(false);
            selectChangeButtons[3].SetActive(false);
            selectChangeSprites[3].SetActive(false);
            selectAllyButtons[2].SetActive(false);
            selectAllySprites[2].SetActive(false);
            selectChangeButtons[2].SetActive(false);
            selectChangeSprites[2].SetActive(false);
            selectAllyButtons[1].SetActive(false);
            selectAllySprites[1].SetActive(false);
            selectChangeButtons[1].SetActive(false);
            selectChangeSprites[1].SetActive(false);
        }
    }


    public void DesactivateAbilities(PartyUnit unit)
    {
        if (CheckAbilityForPosition(unit, unit.abilities[0].GetComponent<AllyAbility>().ReturnPositionList()))
        {
            abilityList[0].interactable = true;
        }
        else
        {
            abilityList[0].interactable = false;
        }
        if (CheckAbilityForPosition(unit, unit.abilities[1].GetComponent<AllyAbility>().ReturnPositionList()))
        {
            abilityList[1].interactable = true;
        }
        else
        {
            abilityList[1].interactable = false;
        }
        if (CheckAbilityForPosition(unit, unit.abilities[2].GetComponent<AllyAbility>().ReturnPositionList()))
        {
            abilityList[2].interactable = true;
        }
        else
        {
            abilityList[2].interactable = false;
        }
        if (CheckAbilityForPosition(unit, unit.abilities[3].GetComponent<AllyAbility>().ReturnPositionList()))
        {
            abilityList[3].interactable = true;
        }
        else
        {
            abilityList[3].interactable = false;
        }
        if (unit.CurrentSpecialCharge() >= 6)
        {
            abilityList[4].interactable = true;
        }
        else
        {
            abilityList[4].interactable = false;
        }

    }

    public bool CheckAbilityForPosition(Unit unit, Transform[] positions)
    {
        foreach (Transform a in positions)
        {
            if (new Vector2(a.position.x, a.position.y) == unit.ReturnPosition())
            {
                return true;
            }
        }

        return false;
    }

    public void DesactivateEnemyPosition()
    {
        foreach (GameObject go in selectEnemyButtons)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in selectEnemySprites)
        {
            go.SetActive(true);
        }

        var enemyUnits = FindObjectsOfType<EnemyUnit>();
        Debug.Log(enemyUnits.Length);
        if (enemyUnits.Length == 3)
        {
            selectEnemyButtons[3].SetActive(false);
            selectEnemySprites[3].SetActive(false);
        }
        else if (enemyUnits.Length == 2)
        {
            selectEnemyButtons[3].SetActive(false);
            selectEnemySprites[3].SetActive(false);

            selectEnemyButtons[2].SetActive(false);
            selectEnemySprites[2].SetActive(false);
        }
        else if (enemyUnits.Length == 1)
        {
            selectEnemyButtons[3].SetActive(false);
            selectEnemySprites[3].SetActive(false);

            selectEnemyButtons[2].SetActive(false);
            selectEnemySprites[2].SetActive(false);

            selectEnemyButtons[1].SetActive(false);
            selectEnemySprites[1].SetActive(false);
        }
    }

}
