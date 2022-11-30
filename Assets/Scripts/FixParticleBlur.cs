using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixParticleBlur : MonoBehaviour
{
    void Awake()
    {
        ParticleSystemRenderer renderer = GetComponent<ParticleSystemRenderer>();
        renderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
    }
}
