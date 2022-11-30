using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleInAnimation : MonoBehaviour
{
    public ParticleSystem particleSystemChoosen;
    public void PlayParticle(){
        particleSystemChoosen.Play();
    }
}
