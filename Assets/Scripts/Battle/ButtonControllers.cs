using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllers : MonoBehaviour
{


    public void Ability1()
    {
        SoundManager.soundManager.PlayButton();
        BattleSystem.battleSystem.ReturnUnitTurn().abilities[0].Act();
    }

    public void Ability2()
    {
        SoundManager.soundManager.PlayButton();
        BattleSystem.battleSystem.ReturnUnitTurn().abilities[1].Act();
    }

    public void Ability3()
    {
        SoundManager.soundManager.PlayButton();
        BattleSystem.battleSystem.ReturnUnitTurn().abilities[2].Act();
    }

    public void Ability4()
    {
        SoundManager.soundManager.PlayButton();
        BattleSystem.battleSystem.ReturnUnitTurn().abilities[3].Act();
    }

    public void SpecialAbility()
    {
        SoundManager.soundManager.PlayButton();
        BattleSystem.battleSystem.ReturnUnitTurn().abilities[4].Act();
    }

    /*public void SelectObjective(int pos)
    {
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnEnemyPositions()[pos].position, BattleSystem.battleSystem.ReturnPositionAttackEnemies()[pos].position);
    }*/
    public void ChangeScene()
    {
        StartCoroutine(ToMapCoroutine());
    }

    public void ToMenu()
    {

        StartCoroutine(ToMenuCoroutine());
    }

    IEnumerator ToMenuCoroutine()
    {
        if (SceneSave.sceneSave != null)
        {
            SceneSave.sceneSave.ReturnToOriginalValues();
            SceneSave.sceneSave.ShowToMap();
        }
        yield return new WaitForSeconds(0.5f);
        SoundManager.soundManager.PlayExplorationMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

    }

    public void Flee()
    {
        var rand = Random.Range(1, 100);

        if(rand <= 70)
        {
            SoundManager.soundManager.StopCombatMusic();
            ChangeScene();
        }
        else
        {

            FindObjectOfType<CanvasController>().ShowDialogueBox();
            FindObjectOfType<CanvasController>().ChangeText("No has conseguido huir con exito.");
        }
    }

    IEnumerator ToMapCoroutine()
    {
        if(SceneSave.sceneSave != null)
        {
            SceneSave.sceneSave.ShowToMap();
        }
        yield return new WaitForSeconds(0.5f);
        SoundManager.soundManager.PlayExplorationMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene("ExplorationScene");

    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void SelectEnemyPos1()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnEnemyPositions()[0].position, BattleSystem.battleSystem.ReturnPositionAttackEnemies()[0].position);
        
    }
    public void SelectEnemyPos2()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnEnemyPositions()[1].position, BattleSystem.battleSystem.ReturnPositionAttackEnemies()[1].position);

    }

    public void SelectEnemyPos3()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnEnemyPositions()[2].position, BattleSystem.battleSystem.ReturnPositionAttackEnemies()[2].position);

    }

    public void SelectEnemyPos4()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnEnemyPositions()[3].position, BattleSystem.battleSystem.ReturnPositionAttackEnemies()[3].position);

    }

    public void SelectAllyPos1()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnAllyPositions()[0].position, BattleSystem.battleSystem.ReturnPositionAttackAllies()[0].position);
    }

    public void SelectAllyPos2()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnAllyPositions()[1].position, BattleSystem.battleSystem.ReturnPositionAttackAllies()[1].position);
    }

    public void SelectAllyPos3()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnAllyPositions()[2].position, BattleSystem.battleSystem.ReturnPositionAttackAllies()[2].position);
    }

    public void SelectAllyPos4()
    {
        SoundManager.soundManager.PlayButton();
        var unit = BattleSystem.battleSystem.ReturnUnitTurn();
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();

        unit.abilities[unit.ReturnAbilitySelcted()].Act2(BattleSystem.battleSystem.ReturnAllyPositions()[3].position, BattleSystem.battleSystem.ReturnPositionAttackAllies()[3].position);
    }

    public void MoveToPos1()
    {
        SoundManager.soundManager.PlayButton();
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2 = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3 = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4 = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;

        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos2.x, pos2.y))
        {
            checkUnit.CheckUnitInPosition(pos1).ChangePosition(pos2);
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos3.x, pos3.y))
        {
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos3);
            checkUnit.CheckUnitInPosition(pos1).ChangePosition(pos2);
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos4.x, pos4.y))
        {
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos4);
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos3);
            checkUnit.CheckUnitInPosition(pos1).ChangePosition(pos2);
        }
        BattleSystem.battleSystem.ReturnUnitTurn().ChangePosition(pos1);
        Debug.Log("Se Mueve a Pos1");
        BattleSystem.battleSystem.NextTurn();
    }

    public void MoveToPos2()
    {
        SoundManager.soundManager.PlayButton();
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2 = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3 = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4 = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;
        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos1.x, pos1.y))
        {
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos1);
            BattleSystem.battleSystem.ReturnUnitTurn().ChangeAxis();
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos3.x, pos3.y))
        {
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos3);
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos4.x, pos4.y))
        {
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos4);
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos3);
        }
        BattleSystem.battleSystem.ReturnUnitTurn().ChangePosition(pos2);
        Debug.Log("Se Mueve a Pos2");
        BattleSystem.battleSystem.NextTurn();

    }

    public void MoveToPos3()
    {
        SoundManager.soundManager.PlayButton();
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2 = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3 = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4 = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;
        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos1.x, pos1.y))
        {
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos1);
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            BattleSystem.battleSystem.ReturnUnitTurn().ChangeAxis();
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos2.x, pos2.y))
        {
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            BattleSystem.battleSystem.ReturnUnitTurn().ChangeAxis();
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos4.x, pos4.y))
        {
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos4);
        }
        BattleSystem.battleSystem.ReturnUnitTurn().ChangePosition(pos3);
        Debug.Log("Se Mueve a Pos3");
        BattleSystem.battleSystem.NextTurn();

    }

    public void MoveToPos4()
    {
        SoundManager.soundManager.PlayButton();
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2 = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3 = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4 = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;

        if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos1.x, pos1.y))
        {
            checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos1);
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            BattleSystem.battleSystem.ReturnUnitTurn().ChangeAxis();

        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos2.x, pos2.y))
        {
            checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            BattleSystem.battleSystem.ReturnUnitTurn().ChangeAxis();
        }
        else if (BattleSystem.battleSystem.ReturnUnitTurn().ReturnPosition() == new Vector2(pos3.x, pos3.y))
        {
            checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            BattleSystem.battleSystem.ReturnUnitTurn().ChangeAxis();
        }

        BattleSystem.battleSystem.ReturnUnitTurn().ChangePosition(pos4);
        Debug.Log("Se Mueve a Pos4");
        BattleSystem.battleSystem.NextTurn();

    }
}
