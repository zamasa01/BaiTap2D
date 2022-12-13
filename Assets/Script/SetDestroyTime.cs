using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestroyTime : MonoBehaviour
{
    public float delayDestroy;
    void Start()
    {
        Invoke("Destroy", delayDestroy);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
