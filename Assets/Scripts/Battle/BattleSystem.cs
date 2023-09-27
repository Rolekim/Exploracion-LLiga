using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum UnitName {  PEPO, DAVO, CONGLO, PAROLO, ENEMY }
public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    int maxUnits;

    public static BattleSystem battleSystem;

    // Units Prefabs
    [SerializeField]
    private GameObject[] allyUnits;
    [SerializeField]
    private GameObject[] enemyUnits;
    private bool victory = false;
    private bool lost = false;

    // Positions
    [SerializeField]
    private Transform qtePosition;
    [SerializeField]
    private Transform[] positionListAllies;
    [SerializeField]
    private Transform[] positionListEnemies;
    [SerializeField]
    private Transform[] positionAttackEnemies;
    [SerializeField]
    private Transform[] positionAttackAllies;

    // Unidades y controladores de turno
    [SerializeField]
    private Unit[] unitsInBattlefield;
    private int arrayPos = -1;
    private bool nextDialogue = false;

    private Unit[] allyUnitsPosition;
    private Unit[] enemyUnitsPosition;

    // Other
    private CanvasController canvasController;

    // SecondsToStartBattle
    [SerializeField]
    private float secondsBeginBattle = 3;

    // Mensajes para el dialogue Box
    [SerializeField][Multiline]
    private string beginBattleMessage;

    //BatleHUD
    [SerializeField]
    private BattleHUD pepoHUD;
    private BattleHUD congloHUD;
    private BattleHUD paroloHUD;
    private BattleHUD davoHUD;

    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float magnitude;

    //Trigger Dialogue
    [SerializeField]
    DialogueTrigger dialogue1;
    [SerializeField]
    DialogueTrigger dialogue2;

    void Awake()
    {
        battleSystem = this;

        StartCoroutine(SoundManager.soundManager.PlayAfterIntro());
        arrayPos = -1;
        canvasController = FindObjectOfType<CanvasController>();
        unitsInBattlefield = new Unit[maxUnits];
        allyUnitsPosition = new Unit[4];
        enemyUnitsPosition = new Unit[4];
        for (int x = 0; x < allyUnits.Length; x++)
        {
            if(allyUnits[x] != null)
            {       
                Instantiate(allyUnits[x], positionListAllies[x]);
            }
            if(enemyUnits[x] != null)
            {
                Instantiate(enemyUnits[x], positionListEnemies[x]);
            }

        }

        GameObject[] allUnits = allyUnits.Concat(enemyUnits).ToArray();

        allyUnitsPosition = FindObjectsOfType<PartyUnit>();
        enemyUnitsPosition = FindObjectsOfType<EnemyUnit>();
        unitsInBattlefield = FindObjectsOfType<Unit>();

        ResizeUnitArray();

    }

    void Start()
    {
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1.1f);

        if (SceneSave.sceneSave != null)
        {
            SceneSave.sceneSave.AddBattleCounter();

            if (SceneSave.sceneSave.ReturnBattleCounter() == 1)
            {
                dialogue1.TriggerDialogue();
            }
            else if (SceneSave.sceneSave.ReturnBattleCounter() == 2)
            {
                dialogue2.TriggerDialogue();
            }
        }

        yield return new WaitForSeconds(0.5f);

        canvasController.ShowDialogueBox();
        canvasController.ChangeText(beginBattleMessage);
        OrderUnits();
    }

    public void Shake()
    {
        StartCoroutine(cameraShake.Shake(duration, magnitude));
    }

    public void NextTurn()
    {
        canvasController.CleanText();

        var enemies = FindObjectsOfType<EnemyUnit>();
        var allies = FindObjectsOfType<PartyUnit>();

        if (enemies.Length == 0)
        {
            victory = true;
            SoundManager.soundManager.StopCombatMusic();
            canvasController.ShowVictory();
        }
        if (allies.Length == 0)
        {
            lost = true;
            SoundManager.soundManager.StopCombatMusic();
            canvasController.ShowDefeat();
        }

        if (!victory && !lost)
        {
            foreach (Unit u in unitsInBattlefield)
            {
                if (u != null)
                {
                    u.ChangeTurnMark(false);
                    u.ChangeOrderInLayer(0);
                }
            }

            nextDialogue = false;

            if (arrayPos < unitsInBattlefield.Length)
            {
                arrayPos += 1;
                Debug.Log(arrayPos);
            }
            else
            {
                arrayPos = 0;
            }

            if (arrayPos == unitsInBattlefield.Length)
            {
                arrayPos = 0;
                unitsInBattlefield = FindObjectsOfType<Unit>();
                OrderUnits();
                if (unitsInBattlefield[arrayPos] != null && !victory && !lost)
                {
                    unitsInBattlefield[arrayPos].GetComponent<Unit>().Turn();
                }
            }
            else
            {
                if (unitsInBattlefield[arrayPos] != null && !victory && !lost)
                {
                    unitsInBattlefield[arrayPos].GetComponent<Unit>().Turn();
                }
            }

            canvasController.ChangeImageButtons();

        }


    }

    public Unit ReturnUnitTurn()
    {
        if(unitsInBattlefield[arrayPos] != null)
        {
            return unitsInBattlefield[arrayPos].GetComponent<Unit>();
        }
        return null;
    }

    public void AllyUnitDead(PartyUnit unit)
    {
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = ReturnAllyPositions()[0].position;
        var pos2 = ReturnAllyPositions()[1].position;
        var pos3 = ReturnAllyPositions()[2].position;
        var pos4 = ReturnAllyPositions()[3].position;

        if (unit.ReturnPosition() == new Vector2(pos1.x, pos1.y))
        {
            if(checkUnit.CheckUnitInPosition(pos2) != null)
            {
                checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos1);
            }
            if(checkUnit.CheckUnitInPosition(pos3) != null)
            {
                checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            }
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }
        }
        else if (unit.ReturnPosition() == new Vector2(pos2.x, pos2.y))
        {
            if (checkUnit.CheckUnitInPosition(pos3) != null)
            {
                checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            }
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }
        }
        else if (unit.ReturnPosition() == new Vector2(pos3.x, pos3.y))
        {
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }
        }


        StartCoroutine(DeathCoroutine(unit));
    }

    public void EnemyUnitDead(EnemyUnit unit)
    {
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        var pos1 = positionListEnemies[0].position;
        var pos2 = positionListEnemies[1].position;
        var pos3 = positionListEnemies[2].position;
        var pos4 = positionListEnemies[3].position;

        if (unit.ReturnPosition() == new Vector2(pos1.x, pos1.y))
        {
            if (checkUnit.CheckUnitInPosition(pos2) != null)
            {
                checkUnit.CheckUnitInPosition(pos2).ChangePosition(pos1);
            }
            if (checkUnit.CheckUnitInPosition(pos3) != null)
            {
                checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            }
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }
        }
        else if (unit.ReturnPosition() == new Vector2(pos2.x, pos2.y))
        {
            if (checkUnit.CheckUnitInPosition(pos3) != null)
            {
                checkUnit.CheckUnitInPosition(pos3).ChangePosition(pos2);
            }
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }
        }
        else if (unit.ReturnPosition() == new Vector2(pos3.x, pos3.y))
        {
            if (checkUnit.CheckUnitInPosition(pos4) != null)
            {
                checkUnit.CheckUnitInPosition(pos4).ChangePosition(pos3);
            }
        }

        StartCoroutine(DeathCoroutine(unit));
        
    }

    IEnumerator DeathCoroutine(Unit unit)
    {
        yield return new WaitForSeconds(0.3f);
        /*if(ReturnUnitTurn() == unit)
        {
            print("Es el turno del que muere");
            NextTurn();
        }*/
        Destroy(unit.gameObject);
        yield return new WaitForSeconds(0.1f);
        NullsToEnd();
        Debug.Log("Llego  aqui");
        ResizeUnitArray();
        
    }

    public void NullsToEnd()
    {
        // Nulls al final
        for (int j = 0; j < unitsInBattlefield.Length; j++)
        {
            if (unitsInBattlefield[j] == null)
            {
                for (int k = j + 1; k < unitsInBattlefield.Length; k++)
                {
                    unitsInBattlefield[k - 1] = unitsInBattlefield[k];
                }
                unitsInBattlefield[unitsInBattlefield.Length - 1] = null;
            }
        }
    }

    void OrderUnits()
    {

        Unit[] tempArray = new Unit[unitsInBattlefield.Length];

        foreach (Unit u in unitsInBattlefield)
        {
            if (u != null)
            {
                u.RefreshSpeed();
            }

        }

        NullsToEnd();
        // Ordena las unidades por su velocidad
        for (int x = 0; x < unitsInBattlefield.Length; x++)
        {
            if (unitsInBattlefield[x] != null)
            {
                for (int y = 0; y < unitsInBattlefield.Length; y++)
                {
                    if (unitsInBattlefield[y] != null)
                    {
                        if (unitsInBattlefield[x].ReturnSpeed() >= unitsInBattlefield[y].ReturnSpeed())
                        {
                            tempArray[x] = unitsInBattlefield[x];
                            unitsInBattlefield[x] = unitsInBattlefield[y];
                            unitsInBattlefield[y] = tempArray[x];
                        }
                    }
                }
            }

        }

    }

    public void ResizeUnitArray()
    {
        var x = 0;

        foreach(Unit u in unitsInBattlefield)
        {
            if(u != null)
            {
                x += 1;
                
            }
        }
        Debug.Log("Resize Array: " + x);
        System.Array.Resize(ref unitsInBattlefield, x);
    }

    public void AddEnemyUnit(GameObject newUnit)
    {
        var added = false;
        for(int x = 0; x < enemyUnits.Length; x++)
        {
            if(enemyUnits[x] == null && !added)
            {
                enemyUnits[x] = newUnit;
                Instantiate(enemyUnits[x], positionListEnemies[x]);
                added = true;
            }
        }

    }

    public bool ReturnNextDialogue()
    {
        return nextDialogue;
    }

    public void ChangeNextDialogue(bool b)
    {
        nextDialogue = b;
    }

    public Transform[] ReturnAllyPositions()
    {
        return positionListAllies;
    }

    public Transform[] ReturnEnemyPositions()
    {
        return positionListEnemies;
    }

    public Unit[] ReturnUnits()
    {
        return unitsInBattlefield;
    }

    public Unit[] ReturnAllyUnitsPosition()
    {
        return allyUnitsPosition;
    }

    public Unit[] ReturnEnemyUnitsPosition()
    {
        return enemyUnitsPosition;
    }

    public Transform[] ReturnPositionAttackEnemies()
    {
        return positionAttackEnemies;
    }

    public Transform[] ReturnPositionAttackAllies()
    {
        return positionAttackAllies;
    }

    public Transform ReturnQtePos()
    {
        return qtePosition;
    }
}
