using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private MeshRenderer warningObject;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource alertSound;
    [SerializeField] private float reloadTime = 1.3f;
    [SerializeField] private float alertTime = 0.4f;
    private Transform playerBodyTarget;
    private Transform playerHeadTarget;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] GameObject balas;
    private bool isPreparingShoot, playerHeadInSight, playerBodyInSight;

    void Start()
    {
        playerBodyTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHeadTarget = GameObject.FindGameObjectWithTag("Head").GetComponent<Transform>();
    }

    void Update()
    {
        gunPivot.LookAt(playerBodyTarget);

        isPlayerInSight();

        if (!isPreparingShoot && (playerHeadInSight || playerBodyInSight))
        {
            StartCoroutine(shootingProcedures());
        }
        else if (isPreparingShoot && (!playerHeadInSight && !playerBodyInSight) && !warningObject.enabled)
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
        yield return new WaitForSeconds(0.2f);
        shootBullet();
        yield return new WaitForSeconds(0.2f);
        shootBullet();
        isPreparingShoot = false;
        warningObject.enabled = false;
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

        if (playerBodyInSight)
        {
            bala.transform.LookAt(playerBodyTarget);
        }
        else if (playerHeadInSight)
        {
            bala.transform.LookAt(playerHeadTarget);
        }

        bala.AddForce(bala.transform.forward *2700);

        shootSound.Play();
    }
    void isPlayerInSight()
    {
        if (Physics.Linecast(shootOrigin.position, playerHeadTarget.transform.position, mask))
        {
            playerHeadInSight = false;
        }
        else
        {
            playerHeadInSight = true;
        }

        if (Physics.Linecast(shootOrigin.position, playerBodyTarget.position, mask))
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
