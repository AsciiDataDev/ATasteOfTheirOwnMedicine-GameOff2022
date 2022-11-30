using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustShootBullets : MonoBehaviour
{
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource alert;
    [SerializeField] private float reloadTime = 0.8f;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private GameObject bullets;
    [SerializeField] private MeshRenderer renderWarning;

    public IEnumerator shootingProcedures()
    {
        yield return new WaitForSeconds(reloadTime);
        renderWarning.enabled = true;
        alert.Play();
        yield return new WaitForSeconds(0.4f);
        renderWarning.enabled = false;
        shootBullet();
        StartCoroutine(shootingProcedures());
    }

    void shootBullet()
    {
        Rigidbody bala = Instantiate(bullets, shootOrigin.position, shootOrigin.rotation).GetComponent<Rigidbody>();
        bala.AddForce(bala.transform.forward * 2700);
        shootSound.Play();
    }
}
