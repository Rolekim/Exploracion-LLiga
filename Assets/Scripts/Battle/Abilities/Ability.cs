using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField]
    private Sprite buttonSprite;

    public virtual void Act()
    {
        return;
    }

    public virtual void Act2(Vector3 attackPoint, Vector3 attackPosicion)
    {
        return;
    }

    public Sprite ReturnSprite()
    {
        return buttonSprite;
    }

}
