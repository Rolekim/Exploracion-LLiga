using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAbility : Ability
{
    public PartyUnit unit;
    [SerializeField]
    private string abilityName;

    // Posiciones desde las que se puede usar la abilidad
    [SerializeField]
    private Transform[] positionList;

    //Info del desplegable
    [SerializeField][Multiline]
    private string abilityDescription;
    [SerializeField]
    private Sprite positions;

    public override void Act()
    {
        var x = FindObjectOfType<ShowObjectives>();

        x.ShowEnemyObjectives();

    }

    public Transform[] ReturnPositionList()
    {
        return positionList;
    }

    public string ReturnAbilityName()
    {
        return abilityName;
    }

    public string ReturnAbilityDescription()
    {
        return abilityDescription;
    }

    public Sprite ReturnPositions()
    {
        return positions;
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

    public void AttackToFirstUnits(int extraDamagePorcentage)
    {
        CheckUnitPosition checkPosition = FindObjectOfType<CheckUnitPosition>();
        var canvas = FindObjectOfType<CanvasController>();
        if (checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[0].position).isHit())
        {
            var damage = InflictDamage(checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[0].position).IsCrit(), extraDamagePorcentage);
            checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[0].position).TakeDamage(damage);
        }
        else
        {
            //canvas.ChangeText(checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[0].position).ReturnName() + " ha evadido el ataque.");
        }

        if (checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[1].position) != null)
        {
            if (checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[0].position).isHit())
            {
                var damage = InflictDamage(checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[1].position).IsCrit(), extraDamagePorcentage);
                checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[1].position).TakeDamage(damage);
            }
            else
            {
                //canvas.ChangeText(checkPosition.CheckUnitInPosition(BattleSystem.battleSystem.ReturnEnemyPositions()[1].position).ReturnName() + " ha evadido el ataque.");
            }
        }

        canvas.ChangeText("Pepo ha realizado un ataque en area y ha aumentado su evasion en 5 puntos");
    }

}
