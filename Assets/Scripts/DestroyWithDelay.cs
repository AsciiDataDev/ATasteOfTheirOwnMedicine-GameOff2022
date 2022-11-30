using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public void DestroyObject(float delay)
    {
        Invoke(nameof(DestroyNow), delay);
    }
    void DestroyNow()
    {
        Destroy(gameObject);
    }
}
