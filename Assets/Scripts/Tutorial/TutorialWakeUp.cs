using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWakeUp : MonoBehaviour
{
    public GameObject textToShow;
    public GameObject textToDestroy;
    public JustShootBullets justShootBullets;
    public Animator animator;
    private float animationDuration = 0.3f;
    private bool entered;

    void TurnNavMeshAndGunOn()
    {
        StartCoroutine(justShootBullets.shootingProcedures());
    }

    void StartWakeUp()
    {
        animator.Play("WakeUp");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !entered)
        {
            Invoke(nameof(TurnNavMeshAndGunOn), animationDuration);
            animator.Play("WakeUp");
            entered = true;
            Destroy(textToDestroy);
            textToShow.SetActive(true);
        }
    }
}
