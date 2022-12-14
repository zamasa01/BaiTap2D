using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawn_Chest : MonoBehaviour
{
    public GameObject[] location;
    public GameObject[] obj;
    public List<GameObject> listLocation;
    public List<GameObject> listLocationHadChest;
    public int maxChestNum;
    public int currentChestNum;
    public int sortingOrderAndLayer;
    public float start = 100f;
    public bool isNumPlused;
    private void Start()
    {
        //Them tat ca vi tri vao chuoi
        foreach (var item in location)
        {
            listLocation.Add(item);
        }
        startSpawn();
    }
    private void Update()
    {
        if(currentChestNum >= maxChestNum)
        {
            stopSpawn();
        }
    }
    public void startSpawn()
    {
        isNumPlused = false;
        InvokeRepeating("spawnChest", 0.1f, 0.1f);
    }
    public void stopSpawn()
    {
        CancelInvoke();
    }
    public void spawnChest()
    {
        int RandNum = (int)Random.Range(0, listLocation.Count - 1);
        if (listLocation[RandNum].GetComponent<DelTriggleStay>().isNearChest == false)
        {
            listLocation[RandNum].SetActive(false);
            Instantiate(obj[0], listLocation[RandNum].transform.parent);
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.position = listLocation[RandNum].transform.position;
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<SortingGroup>().sortingOrder = sortingOrderAndLayer;
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.layer = sortingOrderAndLayer + 5;
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.GetChild(0).gameObject.layer = sortingOrderAndLayer + 5;
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.GetChild(1).gameObject.layer = sortingOrderAndLayer + 5;
            this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.GetChild(1).gameObject.GetComponent<Canvas>().sortingOrder = sortingOrderAndLayer;
            listLocationHadChest.Add(listLocation[RandNum]);
            listLocation[RandNum].SetActive(true);
            listLocation.Remove(listLocation[RandNum]);
            currentChestNum++;
            isNumPlused = true;
            //Debug.Log("Chest had added!!!");
        }
        //Debug.Log("Spawn chest had started!!!");
    }
}
