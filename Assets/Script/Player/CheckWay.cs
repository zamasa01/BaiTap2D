using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWay : MonoBehaviour
{
    public bool onTouch = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && this.gameObject.layer == collision.gameObject.layer)
        {
            onTouch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && this.gameObject.layer == collision.gameObject.layer)
        {
            onTouch = false;
        }
    }
}
