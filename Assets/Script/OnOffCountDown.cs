using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffCountDown : MonoBehaviour
{
    public GameObject Layer2;
    public GameObject Layer5;
    public int Layer2CurrentChest;
    public int Layer2MaxChest;
    public int Layer5CurrentChest;
    public int Layer5MaxChest;
    public bool StartCheck;
    private void Start()
    {
        Layer2MaxChest = Layer2.GetComponent<Spawn_Chest>().maxChestNum;
        Layer5MaxChest = Layer5.GetComponent<Spawn_Chest>().maxChestNum;
        StartCheck = false;
    }
    private void Update()
    {
        if (StartCheck)
        {
            StopCoroutine(startCountDownInStartGame());
            Layer2CurrentChest = Layer2.GetComponent<Spawn_Chest>().currentChestNum;
            Layer5CurrentChest = Layer5.GetComponent<Spawn_Chest>().currentChestNum;
            if (Layer2CurrentChest < Layer2MaxChest || Layer5CurrentChest < Layer5MaxChest)
            {
                if (this.gameObject.transform.GetChild(1).gameObject.active == false)
                {
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            StartCoroutine(startCountDownInStartGame());
        }
    }
    IEnumerator startCountDownInStartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartCheck = true;
        //StopCoroutine(startCountDownInStartGame());
    }
}
