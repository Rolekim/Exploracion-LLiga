using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility : Ability
{
    public EnemyUnit unit;
    [SerializeField]
    private Transform[] positionList;

    public override void Act()
    {
        return;

    }


    public void AttackUnit(Vector2 position, int extraDamagePorcentage)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var canvas = FindObjectOfType<CanvasController>();
        if (checkPosition.CheckUnitInPosition(position).isHit())
        {

            var damage = InflictDamage(checkPosition.CheckUnitInPosition(position).IsCrit(), extraDamagePorcentage);
            canvas.ChangeText(unit.ReturnName() + " ha quitado " + damage + " puntos de vida a " + checkPosition.CheckUnitInPosition(position).ReturnName());

            checkPosition.CheckUnitInPosition(position).TakeDamage(damage);
        }
        else
        {
            canvas.ChangeText(checkPosition.CheckUnitInPosition(position).ReturnName() + " ha evadido el ataque.");
        }

    }

    public int InflictDamage(bool isCrit, int extraDamage)
    {
        int damage;
        if (isCrit)
        {
            damage = (unit.ReturnAttack() + (unit.ReturnAttack() / 2));
            damage += (damage * extraDamage) / 100;
        }
        else
        {
            damage = unit.CalculateDamage(unit.ReturnAttack());
            damage += ((damage * extraDamage) / 100);
        }

        return damage;
    }


    public void AttackAllPartyUnits(int extraDamagePorcentage)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var canvas = FindObjectOfType<CanvasController>();
        var partyUnits = FindObjectsOfType<PartyUnit>();
        foreach (PartyUnit u in partyUnits)
        {
            if (u.isHit())
            {
                var damage = InflictDamage(u.IsCrit(), extraDamagePorcentage);
                u.TakeDamage(damage);
            }
        }

        canvas.ChangeText("Marcelino se ha vuelto loco!");
    }
}
