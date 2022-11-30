using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpAndGo : MonoBehaviour
{
    public AudioSource wakeUpBeep;
    public EnemyNavMesh enemyNavMesh;
    public EnemyShootBullet enemyShootBullet;
    Animator animator;
    public float delayToWakeUp;
    private float animationDuration = 2.2f;

    public void SetWakingUp()
    {
        animator = gameObject.GetComponent<Animator>();
        Invoke(nameof(StartWakeUp), delayToWakeUp);
        Invoke(nameof(TurnNavMeshAndGunOn), delayToWakeUp + animationDuration);
    }

    void TurnNavMeshAndGunOn()
    {
        enemyNavMesh.enabled = true;
        enemyShootBullet.enabled = true;
    }

    void StartWakeUp()
    {
        animator.Play("WakeUp");
        if (wakeUpBeep != null)
        {
            wakeUpBeep.Play();
        }

        CapsuleCollider capsuleCollider = GetComponentInParent<CapsuleCollider>();
        capsuleCollider.height = 1.5f;
        capsuleCollider.center = Vector3.up * 0.27f;
    }
}
