using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Floor_1F : MonoBehaviour
{
    private int previousFloor = 7;
    private int previousSortingLayer = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.layer = previousFloor;
            collision.gameObject.GetComponent<SortingGroup>().sortingOrder = previousSortingLayer;
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.layer = collision.gameObject.layer;
    //        collision.gameObject.GetComponent<SortingGroup>().sortingOrder = collision.gameObject.GetComponent<SortingGroup>().sortingOrder;
    //    }
    //}
}
