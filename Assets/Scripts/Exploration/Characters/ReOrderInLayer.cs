using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReOrderInLayer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer shadowSpriteRenderer;
    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = System.Convert.ToInt32(-transform.position.y);
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
    }
}
