using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Restore_HP : Booster_Base
{
    public void Update()
    {
        base.Update();
        if (plusStat)
        {
            plusStat = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Restore();
        }
    }
    public void Restore()
    {
        whoGotIt.gameObject.GetComponent<scr>().HpCurrent += 40f;
        StartCoroutine(Effective_Time());
    }
    IEnumerator Effective_Time()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(this.gameObject);
    }
}
