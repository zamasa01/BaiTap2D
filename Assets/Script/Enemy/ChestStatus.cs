using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestStatus : MonoBehaviour
{
    public float hpCurrent, heatCurrent;
    public GameObject closeImage, openImage, canvasHpheat, fireEffect;
    private void Start()
    {
        transform.GetComponent<Core>().manaCurrent = 0f;
        transform.GetComponent<Core>().manaRestorePerSecond = -1;
    }
    private void Update()
    {


        //Update HP and Mana realtime
        hpCurrent = transform.GetComponent<Core>().hpCurrent;
        heatCurrent = transform.GetComponent<Core>().manaCurrent;

        //Chest Open
        if(hpCurrent <= 0f && heatCurrent < 100f)
        {
            openChestNormal();
        }
        else if(heatCurrent >= 100f)
        {
            Instantiate(fireEffect, this.transform);
        }
    }
    public void openChestNormal()
    {
        closeImage.SetActive(false);
        canvasHpheat.SetActive(false);
        openImage.SetActive(true);
        transform.GetComponent<Rigidbody2D>().simulated = false;
    }
    public void openChestBurning()
    {

    }
}
