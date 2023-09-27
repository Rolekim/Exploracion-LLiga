using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUnitPosition : MonoBehaviour
{
    private Unit unit;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private Transform deathPosition;
    private bool isInPosition = false;
    private bool running = false;
    private bool isIddle = false;
    void Start()
    {
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        var step = movementSpeed * Time.deltaTime;
        CheckMovement(unit.ReturnAttackPoint(), unit.ReturnPosition(), step);
        
    }

    public void CheckMovement(Vector2 finalPoint, Vector2 initialPoint, float step)
    {
        if (unit.ReturnIsDead())
        {
            DeathMove(unit, step);
        }
        else
        {
            if (unit.ReturnMove())
            {
                MoveUnitToPoint(unit, finalPoint, step);
            }
            else
            {
                MoveUnitToPoint(unit, initialPoint, step);
            }
        }

    }

    public void MoveUnitToPoint(Unit u, Vector2 finalPoint, float step)
    {
        u.transform.position = Vector3.MoveTowards(u.transform.position, finalPoint, step);

        if(u.transform.position == new Vector3(finalPoint.x, finalPoint.y))
        {
            if(finalPoint == u.ReturnPosition())
            {
                if (u.ReturnSpriteRenderer().flipX)
                {
                    u.ChangeAxis();
                }

            }
            u.ResetAnimTrigger("Run");
            u.SetAnimTrigger("Iddle");
            running = false;
        }
        else
        {
            if (!running)
            {
                running = true;
                u.ResetAnimTrigger("Iddle");
                u.SetAnimTrigger("Run");
            }

        }
    }

    public void DeathMove(Unit u, float step)
    {
        u.transform.position = Vector3.MoveTowards(u.transform.position, deathPosition.position, step * 5);

    }
}
