using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Attack_Rate : Booster_Base
{
    public float effectiveTime;
    private float statAttackRate;
    public void Update()
    {
        base.Update();
        if (plusStat)
        {
            plusStat = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            statAttackRate = whoGotIt.gameObject.GetComponent<scr>().TimeAttackDelay * 0.5f;
            buffAttackRate();
        }
    }
    public void buffAttackRate()
    {
        whoGotIt.gameObject.GetComponent<scr>().TimeAttackDelay -= statAttackRate;
        StartCoroutine(Effective_Time());
    }
    IEnumerator Effective_Time()
    {
        yield return new WaitForSecondsRealtime(effectiveTime);
        whoGotIt.gameObject.GetComponent<scr>().TimeAttackDelay += statAttackRate;
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(this.gameObject);
    }
}
