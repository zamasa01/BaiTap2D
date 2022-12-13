using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Burn : MonoBehaviour
{
    public List<GameObject> Player;
    public float damagePerSecond;
    private void Start()
    {
        InvokeRepeating("startBurn", 0f, 0.5f);
    }
    public void startBurn()
    {
        foreach (var item in Player)
        {
            if (item.gameObject.GetComponent<scr>() != null)
            {
                item.gameObject.GetComponent<scr>().HpCurrent -= damagePerSecond;
            }
        }
    }
    public void stopBurn()
    {
        bool onOff = this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().emission.enabled;
        onOff = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.Remove(collision.gameObject);
    }
}
