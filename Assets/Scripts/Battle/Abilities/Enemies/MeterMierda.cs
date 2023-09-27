using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMierda : EnemyAbility
{
    [SerializeField]
    private int speedBuff = 2;
    [SerializeField]
    private int defensePercentafeBuff = 10;

    public override void Act()
    {
        StartCoroutine(ActCoroutine());
    }

    IEnumerator ActCoroutine()
    {
        var canvas = FindObjectOfType<CanvasController>();
        canvas.ShowDialogueBox();
        var checkUnit = FindObjectOfType<CheckUnitPosition>();
        unit.ChangeOrderInLayer(100);
        yield return new WaitForSeconds(1.5f);
        unit.ResetAnimTrigger("Iddle");
        unit.SetAnimTrigger("Ability2");
        yield return new WaitForSeconds(0.3f);
        canvas.ChangeText("La velocidad y la defensa de los enemigos aumenta");
        AllEnemiesDefenseSpeedBuff();
        //ChangeOtherToDamaged(attackPoint.position);
        yield return new WaitForSeconds(1f);
        unit.SetAnimTrigger("Iddle");
    }

    void AllEnemiesDefenseSpeedBuff()
    {
        var enemyUnits = FindObjectsOfType<EnemyUnit>();

        foreach (EnemyUnit u in enemyUnits)
        {
            u.ChangeSpeedBuff(speedBuff);
            u.ChangeDefenseBuff(defensePercentafeBuff);
            u.InstantiatePopupText("+ VEL", "naranja");
            u.InstantiatePopupText("+ DEF", "naranja");
        }
    }
}
