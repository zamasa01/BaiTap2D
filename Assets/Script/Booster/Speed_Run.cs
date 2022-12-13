using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Speed_Run : Booster_Base
{
    public float effectiveTime;
    public bool isActive = false;
    public void Update()
    {
        base.Update();
        if (plusStat)
        {
            plusStat = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            whoGotIt.gameObject.GetComponent<Controller>().speedRun += 2f;
            isActive = true;
            //buffSpeed();
        }
        if (isActive) { effectiveTime -= Time.deltaTime; }
        if (effectiveTime <= 0) 
        {
            whoGotIt.gameObject.GetComponent<Controller>().speedRun -= 2f;
            Destroy(this.gameObject); 
        }
    }
    public void buffSpeed()
    {
        whoGotIt.gameObject.GetComponent<Controller>().speedRun += 2f;
        //StartCoroutine(Effective_Time());
    }
    IEnumerator Effective_Time()
    {
        yield return new WaitForSecondsRealtime(effectiveTime);
        whoGotIt.gameObject.GetComponent<scr>().speed -= 1f;
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(this.gameObject);
    }
}
