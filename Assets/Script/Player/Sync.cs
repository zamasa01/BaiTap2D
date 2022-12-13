using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sync : MonoBehaviour
{
    public GameObject up, down, left, right;
    public Canvas canvas;
    private void Update()
    {
        up.gameObject.layer = this.gameObject.layer;
        down.gameObject.layer = this.gameObject.layer;
        left.gameObject.layer = this.gameObject.layer;
        right.gameObject.layer = this.gameObject.layer;
        canvas.sortingOrder = this.gameObject.GetComponent<SortingGroup>().sortingOrder;
    }
}
