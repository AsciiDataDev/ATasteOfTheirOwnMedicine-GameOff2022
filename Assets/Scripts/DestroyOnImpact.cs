using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public ParticleSystem particleSystemExplode;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0 || col.gameObject.layer == 7 || col.gameObject.layer == 11)
        {
            if (trailRenderer != null)
            {
                trailRenderer.gameObject.transform.SetParent(null);
                trailRenderer.emitting = false;
                trailRenderer.gameObject.GetComponent<DestroyWithDelay>().DestroyObject(3f);
                particleSystemExplode.Play();
            }
            Destroy(gameObject);
        }
    }

}
