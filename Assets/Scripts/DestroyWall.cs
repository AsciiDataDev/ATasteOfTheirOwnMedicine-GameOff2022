using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{

    public SetDelayToWakeUp[] setDelayToWakeUp;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private ParticleSystem explodesParticle;
    [SerializeField] private DestroyWithDelay destroyWithDelayOfParticle;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Shot") || col.gameObject.layer == LayerMask.NameToLayer("Objects"))
        {
            explosionSound.Play();

            explodesParticle.Play();

            destroyWithDelayOfParticle.DestroyObject(3);

            explodesParticle.transform.SetParent(null);

            foreach (SetDelayToWakeUp x in setDelayToWakeUp)
            {
                x.SetScriptsActive();
            }

            Destroy(gameObject);
        }
    }
}
