using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float Time;
    private void OnEnable()
    {
        Invoke("ActiveDestroy", Time);
    }
    private void ActiveDestroy()
    {
        Destroy(this.gameObject);
    }
}
