using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Floor_Base : MonoBehaviour
{
    private int nextFloor = 10;
    private int nextSortingLayer = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.layer = nextFloor;
            collision.gameObject.GetComponent<SortingGroup>().sortingOrder = nextSortingLayer;
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        foreach (Transform child in collision.transform)
    //        {
    //            child.gameObject.layer = collision.gameObject.layer;
    //            child.gameObject.GetComponent<SortingGroup>().sortingOrder = collision.gameObject.GetComponent<SortingGroup>().sortingOrder;
    //        }
    //    }
    //}
}
