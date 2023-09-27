using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESuccess : MonoBehaviour
{
    [SerializeField]
    GameObject qteGO;
    [SerializeField]
    private GameObject aButton;
    private bool canPressKey = true;
    private bool canSuccess = false;
    private bool succes = false;
    [SerializeField]
    private int speed;

    void Start()
    {
        canPressKey = true;
        canSuccess = false;
        succes = false;
    }

    void Update()
    {

        if (Input.GetKeyDown("a") && canSuccess)
        {
            transform.position = aButton.transform.position;
            Debug.Log("Lets goooo");
            SoundManager.soundManager.PlayQTE();
            CheckUnitToUpdateSC(BattleSystem.battleSystem.ReturnUnitTurn());
            succes = true;
            canSuccess = false;
            canPressKey = false;
            StartCoroutine(DestroyQte());
        }
        else if (canPressKey)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }
        else
        {
            StartCoroutine(DestroyQte());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Entrada");
        canSuccess = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log("Salida");
        canPressKey = false;
        canSuccess = false;
    }


    void CheckUnitToUpdateSC(Unit unit)
    {
        var partyUnit = unit.GetComponent<PartyUnit>();
        if (partyUnit != null)
        {
            partyUnit.ChangeCurrentCharge(partyUnit.CurrentSpecialCharge() + 1);
        }

    }

    IEnumerator DestroyQte()
    {
        yield return new WaitForSeconds(1f);

        Destroy(qteGO);
    }
}
