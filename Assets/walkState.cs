using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkState : StateMachineBehaviour
{
    float timer;
    public float walkingTime = 0f;
    Transform player;
    NavMeshAgent agent;
    public float detectionAreaRadius = 18f;
    public float walkSpeed = 2f;
    List<Transform> waypointsList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;

        NPCWaypoints npcWayPointsCluster = animator.GetComponent<NPCWaypoints>();
        GameObject waypointsCluster = npcWayPointsCluster.gameObject;

        foreach (Transform t in waypointsCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 nextPosition = waypointsList[UnityEngine.Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(nextPosition);

        UnityEngine.Debug.Log("Walking to random waypoint...");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[UnityEngine.Random.Range(0, waypointsList.Count)].position);
        }

        timer += Time.deltaTime;

        if (timer > walkingTime)
        {
            animator.SetBool("IsWalk", false);
        }

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("IsChase", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
