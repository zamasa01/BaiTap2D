using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : Booster_Base
{
    public float effectiveTime;
    public void Update()
    {
        base.Update();
        if (plusStat)
        {
            plusStat = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            buffDamage();
        }
    }
    public void buffDamage()
    {
        whoGotIt.gameObject.GetComponent<scr>().damage += 50f;
        StartCoroutine(Effective_Time());
    }
    IEnumerator Effective_Time()
    {
        yield return new WaitForSecondsRealtime(effectiveTime);
        whoGotIt.gameObject.GetComponent<scr>().damage -= 50f;
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(this.gameObject);
    }
}
