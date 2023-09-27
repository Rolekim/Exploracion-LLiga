using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    [SerializeField]
    private string idUnit;
    [SerializeField]
    private string[] nameList;
    void Start()
    {
        var rand = Random.Range(0, nameList.Length);
        ChangeName(nameList[rand]);
    }

    public string ReturnId()
    {
        return idUnit;
    }
    public override void Turn()
    {
        ChangeTurnMark(true);
        ControlUnitBuffs();
        var canvasController = FindObjectOfType<CanvasController>();

        canvasController.CleanText();
        canvasController.ShowDialogueBox();

        Debug.Log("EnemyTurn");
        var rand = Random.Range(0, 5);
        var enemies = FindObjectsOfType<EnemyUnit>();
        if(enemies.Length == 2 && rand == 0)
        {
            rand = 1;
        }

        abilities[rand].Act();

    }

    public override void UnitDeath()
    {
        BattleSystem.battleSystem.EnemyUnitDead(this);
    }
    public override void Turn2()
    {
        base.Turn2();
    }

}
