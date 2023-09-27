using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHit : MonoBehaviour
{
    [SerializeField]
    private float seconds;

    void Start()
    {
        StartCoroutine(DestroyCoroutine(seconds));
    }

    IEnumerator DestroyCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(this.gameObject);
    }
}
