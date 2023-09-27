using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownAbilities : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleAbility;
    [SerializeField]
    private TextMeshProUGUI descriptionAbility;

    [SerializeField]
    private SpriteRenderer spritePositions;

    public void SetNameAndDescription(string title, string description, Sprite positions)
    {
        titleAbility.text = title;
        descriptionAbility.text = description;
        spritePositions.sprite = positions;
    }

}
