using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_Base : MonoBehaviour
{
    protected Vector3 stepScale;
    public GameObject whoGotIt;
    public float speed;
    public bool gotIt;
    public bool plusStat;
    Coroutine started;
    public void Start()
    {
        gotIt = false;
        plusStat = false;
        stepScale = new Vector3(0.01f, 0.01f, 0.0f);
        started = StartCoroutine(curveMovement(6f, 80f * Mathf.Deg2Rad, gameObject.transform.position));
        Invoke("stopCoroutine", 1.2f);
    }
    public void Update()
    {
        
        if (gotIt)
        {
            if (this.gameObject.transform.localScale.x > 0f)
            {
                this.gameObject.transform.localScale -= stepScale;
            }
            Vector3 follower = this.gameObject.transform.position;
            Vector3 target = whoGotIt.gameObject.transform.position;
            this.gameObject.transform.position = Vector3.Lerp(follower, target, speed * Time.deltaTime);
        }
    }
    public void stopCoroutine()
    {
        StopCoroutine(started);
    }
    IEnumerator curveMovement(float v0, float angle, Vector3 realPosition)
    {
        float t = 0;
        while (t < 100)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            transform.position = new Vector3(realPosition.x + x, realPosition.y + y, 0f);
            t += Time.deltaTime;
            yield return null;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            whoGotIt = collision.gameObject;
            gotIt = true;
            plusStat = true;
        }
    }
}
