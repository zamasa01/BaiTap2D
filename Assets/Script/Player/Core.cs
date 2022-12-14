using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    public float hpCurrent;
    public Slider hpSlider;
    public float hpRestorePerSecond;
    public float manaCurrent;
    public Slider manaSlider;
    public float manaRestorePerSecond;
    public float heatCurrent;
    public Slider heatSlider;
    public float heatSubtractPerSecond;
    public float damage;
    public float attackSpeed;

    public void Awake()
    {
        //Set gia tri mau va mana ban dau
        hpSlider.maxValue = hpCurrent;
        manaSlider.maxValue = manaCurrent;
        heatSlider.maxValue = heatCurrent;

        //Hoi mau va mana tren giay
        InvokeRepeating("restorePerSecond", 0f, 1f);
    }
    public void Start()
    {
        heatCurrent = 0f;
    }
    public void Update()
    {
        //Effect mau va mana giam tu tu
        Smoothly();

        //Hide or Show heat slider
        if(heatCurrent > 0f)
        {
            heatSlider.gameObject.SetActive(true);
        }
        else
        {
            heatSlider.gameObject.SetActive(false);
        }
    }
    public void Smoothly()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, hpCurrent, 7 * Time.deltaTime);
        manaSlider.value = Mathf.Lerp(manaSlider.value, manaCurrent, 7 * Time.deltaTime);
        heatSlider.value = Mathf.Lerp(heatSlider.value, heatCurrent, 7 * Time.deltaTime);
    }
    public void restorePerSecond()
    {
        if (hpCurrent >= hpSlider.maxValue) 
        { hpCurrent = hpSlider.maxValue; } 
        else if(hpCurrent <= hpSlider.minValue)
        { hpCurrent = hpSlider.minValue; }
        else 
        { hpCurrent += hpRestorePerSecond; }

        if (manaCurrent >= manaSlider.maxValue)
        { manaCurrent = manaSlider.maxValue; }
        else if (manaCurrent <= manaSlider.minValue)
        { manaCurrent = manaSlider.minValue; }
        else
        { manaCurrent += manaRestorePerSecond; }

        if (heatCurrent >= heatSlider.maxValue)
        { heatCurrent = heatSlider.maxValue; heatCurrent += heatSubtractPerSecond; }
        else if (heatCurrent <= heatSlider.minValue)
        { heatCurrent = heatSlider.minValue; }
        else
        { heatCurrent += heatSubtractPerSecond; }
    }
}
