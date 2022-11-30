using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public float enemyQuantity;
    public float kills;
    [SerializeField] private Animator animator;
    void Awake()
    {
        if (kills >= enemyQuantity)
        {
            OpenDoor();
        }
    }

    public void EnemyKilled()
    {
        kills++;
        if (kills >= enemyQuantity)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        animator.SetTrigger("Open");
    }
}
