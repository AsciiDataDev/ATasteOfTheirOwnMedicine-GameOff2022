using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private ParticleSystem explodesParticle;
    [SerializeField] private DestroyWithDelay destroyWithDelayOfParticle;
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Shot"))
        {
            explosionSound.Play();

            explodesParticle.Play();

            destroyWithDelayOfParticle.DestroyObject(3);

            explodesParticle.transform.SetParent(null);

            GameObject.FindObjectOfType<EnemyTracker>().EnemyKilled();

            Destroy(gameObject);
        }
    }
}
