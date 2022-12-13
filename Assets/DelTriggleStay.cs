using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelTriggleStay : MonoBehaviour
{
    public bool isNearLocationChest;
    public bool isNearChest;
    public List<GameObject> LocationChestsNear;
    public List<GameObject> ChestsNear;
    void Start()
    {
        isNearLocationChest = false;
        isNearChest = false;
    }
    void Update()
    {

        if (LocationChestsNear.Count <= 0) { isNearLocationChest = false; } else { isNearLocationChest = true; }
        if (ChestsNear.Count <= 0) { isNearChest = false; } else { isNearChest= true; }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Check locations chest nearby
        bool a = true;
        foreach (var item in LocationChestsNear)
        {
            if(collision.transform.position == item.transform.position) { a = false; break; }
        }
        if(a) { LocationChestsNear.Add(collision.gameObject); }

        //Check chest nearby
        bool checkListInLayer = false;
        bool checkListInThisO = false;
        foreach (var item in this.gameObject.transform.parent.GetComponent<Spawn_Chest>().listLocationHadChest)
        {
            if (collision.transform.position == item.transform.position)
            {
                checkListInLayer = true;
                break;
            }
        }
        foreach (var item in ChestsNear)
        {
            if (collision.transform.position == item.transform.position)
            {
                checkListInThisO = true;
                break;
            }
        }
        if (checkListInLayer == true && checkListInThisO == false) { ChestsNear.Add(collision.gameObject); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var item in LocationChestsNear)
        {
            if(collision.transform.position == item.transform.position)
            {
                LocationChestsNear.Remove(collision.gameObject);
                ChestsNear.Remove(collision.gameObject);
                break;
            }
        }
    }
    public void DestroyObj()
    {
        foreach (var item in LocationChestsNear)
        {
            Destroy(item);
        }
    }
}
