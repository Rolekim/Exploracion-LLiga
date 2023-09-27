using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSave : MonoBehaviour
{
    public static SceneSave sceneSave;
    [SerializeField]
    private int battleCounter = 0;
    [SerializeField]
    Vector3 playerPosition;
    [SerializeField]
    Vector3 player2Position;
    [SerializeField]
    Vector3 player3Position;
    [SerializeField]
    Vector3 player4Position;

    Vector3 initialPlayerPos;
    Vector3 initialPlayer2Pos;
    Vector3 initialPlayer3Pos;
    Vector3 initialPlayer4Pos;

    int posSaved = 0;
    [SerializeField]
    Animator battleTransition;

    bool canMove = false;

    public bool finalBattle;
    public bool inDialogue;
    public bool firstDialogue = true;

    
    void Awake()
    {
        if (sceneSave == null)
        {
            sceneSave = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ShowTransition()
    {
        battleTransition.SetTrigger("Show");
    }

    public void ShowToMap()
    {
        battleTransition.SetTrigger("ReturnToMap");
    }

    public void RegisterInitialPositions(GameObject player, GameObject player2, GameObject player3, GameObject player4)
    {
        initialPlayerPos = player.transform.position;
        initialPlayer2Pos = player2.transform.position;
        initialPlayer3Pos = player3.transform.position;
        initialPlayer4Pos = player4.transform.position;
    }

    public void PlayerIsSwitchingScene(GameObject player, GameObject player2, GameObject player3, GameObject player4)
    {
        playerPosition = player.transform.position;
        player2Position = player2.transform.position;
        player3Position = player3.transform.position;
        player4Position = player4.transform.position;


        posSaved = 1;
        // Player Switches Scene
    }
    public void PlayerIsComingBack(GameObject player, GameObject player2, GameObject player3, GameObject player4)
    {
        player2.transform.position = player2Position;
        player3.transform.position = player3Position;
        player4.transform.position = player4Position;
        player.transform.position = playerPosition;
    }

    public void MoveCamera(GameObject player)
    {
        player.transform.position = playerPosition;
    }

    public Vector3 ReturnPlayerPosition()
    {
        return playerPosition;
    }

    public int ReturValue()
    {
        return posSaved;
    }

    public bool ReturnCanMove()
    {
        return canMove;
    } 

    public void ChangeCanMove(bool statement)
    {
        canMove = statement;
    }

    public int ReturnBattleCounter()
    {
        return battleCounter;
    }

    public void AddBattleCounter()
    {
        battleCounter += 1;
    }

    public void ReturnToOriginalValues()
    {
        battleCounter = 0;
        firstDialogue = true;
        canMove = false;
        finalBattle = false;
        inDialogue = false;

        playerPosition = initialPlayerPos;
        player2Position = initialPlayer2Pos;
        player3Position = initialPlayer3Pos;
        player4Position = initialPlayer4Pos;
    }
}
