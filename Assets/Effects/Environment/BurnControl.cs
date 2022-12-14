using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnControl : MonoBehaviour
{
    public GameObject fireEffect, fireAttack;
    public float timeToDestroy;
    public void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 1f)
        {
            fireEffect.GetComponent<ParticleSystem>().loop = false;
            fireAttack.GetComponent<ParticleSystem>().loop = false;
        }
        if(timeToDestroy <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
