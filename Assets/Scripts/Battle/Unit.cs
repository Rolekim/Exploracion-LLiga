using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private GameObject qteGo;
    [SerializeField]
    private string unitName;
    [SerializeField]
    private UnitName unitNick;
    private BattleHUD battleHUD;
    private ControlUnitPosition controlPosition;
    private bool isDead = false;
    [SerializeField]
    private bool move = false;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private int numberPosition;
    [SerializeField]
    private Vector2 attackPoint;
    private int abilitySelected;
    [SerializeField]
    private GameObject turnMark;

    public Ability[] abilities;

    //        * STATS *
    private int attackDamage;
    [SerializeField]
    private int initialAttackDamage;

    [SerializeField]
    private int maxHP;

    private int currentHP;

    private int speed;
    [SerializeField]
    private int initialSpeed;


    private int crit;
    [SerializeField]
    private int initialCrit;


    private int evasion;
    [SerializeField]
    private int initialEvasion;

    [SerializeField]
    private int defensePercentage;

    // BUFFS
    private bool protectHitBuff = false;
    private bool defenseBuff = false;
    private int defenseBuffTurns = 0;
    private bool attackBuff = false;
    private int attackBuffTurns = 0;
    private bool speedBuff = false;
    private int speedBuffTurns = 0;
    private bool critBuff = false;
    private int critBuffTurns = 0;
    private bool evasionBuff = false;
    private int evasionBuffTurns = 0;

    public Transform pfDamagePopup;
    public Transform popupPoint;

    [SerializeField]
    protected GameObject hitEffect;

    void Awake()
    {
        ChangeTurnMark(false);
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        controlPosition = GetComponent<ControlUnitPosition>();
        currentHP = maxHP;
        crit = initialCrit;
        evasion = initialEvasion;
        speed = initialSpeed;
        attackDamage = initialAttackDamage;
        position = transform.position;

        var battleHUDs = FindObjectsOfType<BattleHUD>();

        foreach (BattleHUD b in battleHUDs)
        {
            if (b.ReturnUnitName() == unitNick)
            {
                battleHUD = b;
            }
        }
    }

    public bool ReturnIsDead()
    {
        return isDead;
    }

    public virtual void Turn()
    {
        return;
    }
    public virtual void Turn2()
    {
        return;
    }

    public virtual void UnitDeath()
    {
        return;
    }

    public void ChangeName(string name)
    {
        unitName = name;
    }

    public void TakeDamage(int dmg)
    {
        if (defenseBuff)
        {
            dmg -= ((dmg * defensePercentage) / 100);
        }

        currentHP -= dmg;
        SetAnimTrigger("Damaged");
        ResetAnimTrigger("Iddle");
        InstantiatePopup("rojo", dmg);
        BattleSystem.battleSystem.Shake();
        var createHit = Instantiate(hitEffect, spriteRenderer.transform.position, spriteRenderer.transform.rotation);
        if (battleHUD != null)
        {
            battleHUD.SetHUD(this);
        }

        if (currentHP <= 0)
        {
            isDead = true;
            UnitDeath();
            Debug.Log("Mori xd");
        }
    }


    public void Heal(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        Debug.Log("Cura");
        SoundManager.soundManager.PlayHeal();
        InstantiatePopup("verde", heal);
        if (battleHUD != null)
        {
            battleHUD.SetHUD(this);
        }
    }

    public void ChangeTurnMark(bool value)
    {
        turnMark.SetActive(value);
    }

    public int CalculateDamage(int attackDamage)
    {
        var rand = Random.Range(attackDamage - (attackDamage / 4), attackDamage + (attackDamage / 4));

        return rand;
    }

    public bool isHit()
    {
        // Faltan cosas (temporal)
        var rand = Random.Range(0, 100);

        if (rand <= evasion)
        {
            Debug.Log("Evasion");
            InstantiatePopupText("FALLO", "blanco");
            SoundManager.soundManager.PlayEvasion();
            return false;
        }
        else
        {
            if (protectHitBuff)
            {
                protectHitBuff = false;
                InstantiatePopupText("Protegido", "blanco");
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool IsCrit()
    {
        var rand = Random.Range(0, 100);
        if (rand <= crit)
        {
            SoundManager.soundManager.PlayCrit();
            Debug.Log("Critico");
            return true;
        }
        else
        {
            Debug.Log("No Critico");
            SoundManager.soundManager.PlayHit();
            return false;
        }
    }

    public void RefreshSpeed()
    {
        speed = initialSpeed;
    }

    public void RefreshPosition()
    {
        position = this.transform.position;
    }
    public Vector2 ReturnPosition()
    {
        return position;
    }

    public int ReturnSpeed()
    {
        return speed;
    }

    public string ReturnName()
    {
        return unitName;
    }

    public void DoAbility(int abilityNumber)
    {
        abilities[abilityNumber].Act();
    }

    public int ReturnAttack()
    {
        return attackDamage;
    }

    public int ReturnMaxHp()
    {
        return maxHP;
    }

    public int ReturnAbilitySelcted()
    {
        return abilitySelected;
    }

    public void ChangeAbilitySelected(int abilityNumber)
    {
        abilitySelected = abilityNumber;
    }

    public int ReturnHp()
    {
        return currentHP;
    }

    public int ReturnCrit()
    {
        return crit;
    }

    public ControlUnitPosition ReturnControlUnitPosition()
    {
        return controlPosition;
    }

    public bool ReturnMove()
    {
        return move;
    }

    public void ChangeMove()
    {
        move = !move;
    }

    public Vector2 ReturnAttackPoint()
    {
        return attackPoint;
    }

    public int ReturnNumberPosition()
    {
        return numberPosition;
    }

    public void ChangePosition(Vector3 pos)
    {
        position = pos;
    }

    public void ChangeAttackPoint(Vector2 atckPoint)
    {
        attackPoint = atckPoint;
    }

    public void ChangeProtectionBuff()
    {
        protectHitBuff = true;
        Debug.Log("Protection buff");
    }

    public void SetAnimTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void ResetAnimTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }

    public void ChangeOrderInLayer(int order)
    {
        var pos1Ally = BattleSystem.battleSystem.ReturnAllyPositions()[0].position;
        var pos2Ally = BattleSystem.battleSystem.ReturnAllyPositions()[1].position;
        var pos3Ally = BattleSystem.battleSystem.ReturnAllyPositions()[2].position;
        var pos4Ally = BattleSystem.battleSystem.ReturnAllyPositions()[3].position;

        var pos1Enemy = BattleSystem.battleSystem.ReturnEnemyPositions()[0].position;
        var pos2Enemy = BattleSystem.battleSystem.ReturnEnemyPositions()[1].position;
        var pos3Enemy = BattleSystem.battleSystem.ReturnEnemyPositions()[2].position;
        var pos4Enemy = BattleSystem.battleSystem.ReturnEnemyPositions()[3].position;

        if (position == new Vector2(pos1Ally.x, pos1Ally.y) || position == new Vector2(pos1Enemy.x, pos1Enemy.y))
        {
            spriteRenderer.sortingOrder = 2;
        }
        else if (position == new Vector2(pos2Ally.x, pos2Ally.y) || position == new Vector2(pos2Enemy.x, pos2Enemy.y))
        {
            spriteRenderer.sortingOrder = 3;
        }
        else if (position == new Vector2(pos3Ally.x, pos3Ally.y) || position == new Vector2(pos3Enemy.x, pos3Enemy.y))
        {
            spriteRenderer.sortingOrder = 4;
        }
        else if (position == new Vector2(pos4Ally.x, pos4Ally.y) || position == new Vector2(pos4Enemy.x, pos4Enemy.y))
        {
            spriteRenderer.sortingOrder = 5;
        }
    }

    public void ChangeOrderToValue(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    public int ReturnOrderInLayer()
    {
        return spriteRenderer.sortingOrder;
    }

    public void ChangeAxis()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public SpriteRenderer ReturnSpriteRenderer()
    {
        return spriteRenderer;
    }

    public void ControlUnitBuffs()
    {
        if (attackBuff)
        {
            attackBuffTurns += 1;
            if (attackBuffTurns > 2)
            {
                attackBuff = false;
                attackBuffTurns = 0;
                attackDamage = initialAttackDamage;
                Debug.Log("Fin del buff");

            }
        }

        if (speedBuff)
        {
            speedBuffTurns += 1;
            if (speedBuffTurns > 2)
            {
                speedBuff = false;
                speedBuffTurns = 0;
                speed = initialSpeed;
                Debug.Log("Fin del buff");
            }
        }

        if (critBuff)
        {
            critBuffTurns += 1;
            if (critBuffTurns > 2)
            {
                critBuff = false;
                critBuffTurns = 0;
                crit = initialCrit;
                Debug.Log("Fin del buff");
            }
        }
        if (evasionBuff)
        {
            evasionBuffTurns += 1;
            if (evasionBuffTurns > 2)
            {
                evasionBuff = false;
                evasionBuffTurns = 0;
                evasion = initialEvasion;
                Debug.Log("Fin del buff");
            }
        }

        if (defenseBuff)
        {
            defenseBuffTurns += 1;
            if (defenseBuffTurns > 2)
            {
                defenseBuff = false;
                defenseBuffTurns = 0;
                defensePercentage = 0;
                Debug.Log("Fin del buff");
            }
        }
    }

    public void ChangeSpeedBuff(int value)
    {
        SoundManager.soundManager.PlayBuff();
        speedBuff = true;
        speed += value;
        speedBuffTurns = 0;
        Debug.Log(unitName + ": buff velocidad");
    }

    public void ChangeAttackBuff(int value)
    {
        SoundManager.soundManager.PlayBuff();
        attackBuff = true;
        attackDamage += value;
        attackBuffTurns = 0;
        Debug.Log(unitName + ": buff ataque");
    }

    public void Plus1TurnAttackBuff()
    {
        attackBuffTurns = 1;
    }

    public void ChangeCritBuff(int value)
    {
        SoundManager.soundManager.PlayBuff();
        critBuff = true;
        crit += value;
        critBuffTurns = 0;
        Debug.Log(unitName + ": buff critico");
    }

    public void ChangeEvasionBuff(int value)
    {
        SoundManager.soundManager.PlayBuff();
        evasionBuff = true;
        evasion += value;
        evasionBuffTurns = 0;
        Debug.Log(unitName + ": buff evasion");
    }

    public void ChangeDefenseBuff(int value)
    {
        SoundManager.soundManager.PlayBuff();
        defenseBuff = true;
        defensePercentage += value;
        defenseBuffTurns = 0;
        Debug.Log(unitName + ": buff defensa");
    }

    public GameObject ReturnQteGo()
    {
        return qteGo;
    }

    public BattleHUD ReturnBattleHUD()
    {
        return battleHUD;
    }

    void InstantiatePopup(string color, int value)
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, popupPoint.position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(value, color);
    }

    public void InstantiatePopupText(string message, string color)
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, popupPoint.position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.SetupText(message, color);
    }

}
