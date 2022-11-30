using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShootBullet : MonoBehaviour
{
    [SerializeField] private EnemyNavMesh enemyNavMesh;
    [SerializeField] private MeshRenderer warningObject;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource alertSound;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private float alertTime = 0.25f;
    private Transform playerBodyTarget;
    private Transform playerHeadTarget;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] GameObject balas;
    private bool isPreparingShoot;
    void OnEnable()
    {
        playerBodyTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHeadTarget = GameObject.FindGameObjectWithTag("Head").GetComponent<Transform>();
    }

    void Update()
    {
        gunPivot.LookAt(playerBodyTarget);

        if (!isPreparingShoot && (enemyNavMesh.playerHeadInSight || enemyNavMesh.playerBodyInSight))
        {
            StartCoroutine(shootingProcedures());
        }
        else if (isPreparingShoot && (!enemyNavMesh.playerHeadInSight && !enemyNavMesh.playerBodyInSight) && !warningObject.enabled)
        {
            StopBeforeWarning();
        }
    }


    void StopBeforeWarning()
    {
        StopAllCoroutines();
        isPreparingShoot = false;
    }

    IEnumerator shootingProcedures()
    {
        startPreparation();
        yield return new WaitForSeconds(reloadTime);
        showWanings();
        yield return new WaitForSeconds(alertTime);
        shootBullet();
    }
    void startPreparation()
    {
        isPreparingShoot = true;
    }
    void showWanings()
    {
        alertSound.Play();
        warningObject.enabled = true;
    }
    void shootBullet()
    {
        Rigidbody bala = Instantiate(balas, shootOrigin.position, shootOrigin.rotation).GetComponent<Rigidbody>();

        if (enemyNavMesh.playerBodyInSight)
        {
            bala.transform.LookAt(playerBodyTarget);
        }
        else if (enemyNavMesh.playerHeadInSight)
        {
            bala.transform.LookAt(playerHeadTarget);
        }

        bala.AddForce(bala.transform.forward * 2700);

        shootSound.Play();

        isPreparingShoot = false;
        warningObject.enabled = false;
    }
}
