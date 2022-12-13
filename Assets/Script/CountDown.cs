using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    public GameObject layer2;
    public GameObject layer5;
    public int seconTimeToRespawnTheChest;
    public int currentChestNumLayer2;
    public int currentChestNumLayer5;
    public int maxChestNumLayer2;
    public int maxChestNumLayer5;
    public void OnEnable()
    {
        text.text = seconTimeToRespawnTheChest.ToString();
        slider.maxValue = seconTimeToRespawnTheChest;
        slider.value = seconTimeToRespawnTheChest;
        maxChestNumLayer2 = layer2.GetComponent<Spawn_Chest>().maxChestNum;
        maxChestNumLayer5 = layer5.GetComponent<Spawn_Chest>().maxChestNum;
        InvokeRepeating("countDown", 0f, 1f);
    }
    public void Update()
    {
        currentChestNumLayer2 = layer2.GetComponent<Spawn_Chest>().currentChestNum;
        currentChestNumLayer5 = layer5.GetComponent<Spawn_Chest>().currentChestNum;
        if (slider.value == 0f)
        {
            layer2.GetComponent<Spawn_Chest>().isNumPlused = false;
            layer5.GetComponent<Spawn_Chest>().isNumPlused = false;
            slider.value = slider.maxValue;
            CancelInvoke();
            if (currentChestNumLayer2 < maxChestNumLayer2 && currentChestNumLayer5 == maxChestNumLayer5)
            {
                layer2.GetComponent<Spawn_Chest>().startSpawn();
                Debug.Log("Spawn chest Layer2!!!");
            }
            else if (currentChestNumLayer2 == maxChestNumLayer2 && currentChestNumLayer5 < maxChestNumLayer5)
            {
                layer5.GetComponent<Spawn_Chest>().startSpawn();
                Debug.Log("Spawn chest Layer5!!!");
            }
            else if (currentChestNumLayer2 < maxChestNumLayer2 && currentChestNumLayer5 < maxChestNumLayer5)
            {
                int Rand = (int)Random.Range(1, 10);
                if (Rand % 2 == 0)
                { layer2.GetComponent<Spawn_Chest>().startSpawn(); }
                else
                { layer5.GetComponent<Spawn_Chest>().startSpawn(); }
                Debug.Log("Spawn chest Random!!!");
            }
            Invoke("checkReSetCountDown", 1f);
        }
        if (layer2.GetComponent<Spawn_Chest>().isNumPlused || layer5.GetComponent<Spawn_Chest>().isNumPlused)
        {
            layer2.GetComponent<Spawn_Chest>().stopSpawn();
            layer5.GetComponent<Spawn_Chest>().stopSpawn();
        }
    }
    public void checkReSetCountDown()
    {
        if (currentChestNumLayer2 < maxChestNumLayer2 || currentChestNumLayer5 < maxChestNumLayer5)
        {
            //slider.value = slider.maxValue;
            text.text = slider.maxValue.ToString();
            InvokeRepeating("countDown", 0f, 1f);
        }
        else if (currentChestNumLayer2 >= maxChestNumLayer2 && currentChestNumLayer5 >= maxChestNumLayer5)
        {
            StartCoroutine(StopCountDown());
        }
    }
    private IEnumerator StopCountDown()
    {
        //slider.value = slider.maxValue;
        this.gameObject.GetComponent<Animator>().SetInteger("Execute", 2);
        yield return new WaitForSecondsRealtime(0.6f);
        StopCoroutine(StopCountDown());
        this.gameObject.SetActive(false);
    }
    private void countDown()
    {
        slider.value -= 1;
        text.text = slider.value.ToString();
        if(slider.value <= 5)
        {
            StartCoroutine(Shake());
            if(slider.value <= 0) { StopCoroutine(Shake()); }
        }
    }
    private IEnumerator Shake()
    {
        this.gameObject.GetComponent<Animator>().SetInteger("Execute", 1);
        yield return new WaitForSecondsRealtime(0.1f);
        this.gameObject.GetComponent<Animator>().SetInteger("Execute", 0);
        yield break;
    }
    private IEnumerator RestartComponent()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        this.gameObject.GetComponent<CountDown>().enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        this.gameObject.GetComponent<CountDown>().enabled = true;
        yield break;
    }
}
