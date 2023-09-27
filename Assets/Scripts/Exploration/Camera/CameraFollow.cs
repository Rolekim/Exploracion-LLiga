using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    private Vector3 offset;
    [SerializeField][Range(1, 10)]
    private float smoothFactor;
    // Update is called once per frame

    void Start()
    {
        var sceneSaves = FindObjectsOfType<SceneSave>();

        foreach (SceneSave s in sceneSaves)
        {
            if (s.ReturValue() == 1)
            {
                s.MoveCamera(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
