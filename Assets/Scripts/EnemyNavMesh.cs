using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Animator animator;
    public float DistanceFromTargetToStop;
    public Transform BulletSpawnLocation;
    public LayerMask mask;
    public Transform positionToGo;
    public Transform cam;
    public NavMeshAgent navMeshAgent;
    [HideInInspector] public bool playerHeadInSight;
    [HideInInspector] public bool playerBodyInSight;
    void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        positionToGo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        cam = GameObject.FindGameObjectWithTag("Head").GetComponent<Transform>();
    }
    void Update()
    {
        navMeshAgent.SetDestination(positionToGo.position);

        isPlayerInSight();

        bool isPlayerCloseEnough = Vector3.Distance(positionToGo.position, transform.position) < DistanceFromTargetToStop;

        if (((playerHeadInSight || playerBodyInSight) && isPlayerCloseEnough))
        {
            //gets a little bit nearer before stopping, so it doesnt stop too early and bullets hit corners of walls
            Invoke(nameof(StopNavMeshAgent), 0.2f);
        }
        else if (navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial && navMeshAgent.remainingDistance < 1)
        {
            StopNavMeshAgent();
        }
        else
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("Walking", true);
        }

    }

    void StopNavMeshAgent()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("Walking", false);
    }

    void isPlayerInSight()
    {
        if (Physics.Linecast(BulletSpawnLocation.position, cam.transform.position, mask))
        {
            playerHeadInSight = false;
        }
        else
        {
            playerHeadInSight = true;
        }

        if (Physics.Linecast(BulletSpawnLocation.position, positionToGo.position, mask))
        {
            playerBodyInSight = false;
        }
        else
        {
            playerBodyInSight = true;
        }
        //Debug.Log("head: " + playerHeadInSight + "  body: " + playerBodyInSight);
    }
}
