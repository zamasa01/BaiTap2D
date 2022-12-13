using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Heal_per_Second : Booster_Base
{
    public float effectiveTime;
    public void Update()
    {
        base.Update();
        if (plusStat)
        {
            plusStat = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Restore_HP_Per_Second();
        }
    }
    public void Restore_HP_Per_Second()
    {
        whoGotIt.gameObject.GetComponent<scr>().HP_Restore_per_Second += 9f;
        StartCoroutine(Effective_Time());
    }
    IEnumerator Effective_Time()
    {
        yield return new WaitForSecondsRealtime(effectiveTime);
        whoGotIt.gameObject.GetComponent<scr>().HP_Restore_per_Second -= 9f;
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(this.gameObject);
    }
}
