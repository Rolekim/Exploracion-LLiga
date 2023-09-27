using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    int pos;
    [SerializeField]
    float speed = 3;
    [SerializeField]
    GameObject characToFollow;
    [SerializeField]
    float targetDistance;
    [SerializeField]
    float allowedDistance = 1.1f;
    private RaycastHit2D shot;
    public Vector3 raycastDir;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        raycastDir = characToFollow.transform.position - transform.position;

        if(Physics2D.Raycast(transform.position, raycastDir))
        {
            targetDistance = Vector3.Distance(characToFollow.transform.position, transform.position);

            if(targetDistance >= allowedDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, characToFollow.transform.position, speed * Time.deltaTime);
            }
            else
            {

            }
        }
        else
        {
            print("No entra aqui");
        }
    }


    void Update()
    {
        Debug.DrawRay(transform.position, raycastDir, Color.green);
    }

    public void UpdateAnimatorAxis(float xaxis, float yaxis, float lastXAxis, float lastYAxis)
    {
        anim.SetFloat("yinput", yaxis);
        anim.SetFloat("xinput", xaxis);
        anim.SetFloat("lastYInput", lastYAxis);
        anim.SetFloat("lastXInput", lastXAxis);
    }

}
