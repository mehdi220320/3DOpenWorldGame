using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class idleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 4f;
    Transform player;
    public float detectionAreRadius = 18f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("IsWalk", true);

        }
        float distanceFromPlater = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlater < detectionAreRadius)
        {
            animator.SetBool("IsChase", true);
        }
    }

    /* override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

     }*/
}
