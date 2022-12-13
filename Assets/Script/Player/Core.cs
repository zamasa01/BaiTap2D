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
    public float damage;
    public float attackSpeed;

    public void Start()
    {
        //Set gia tri mau va mana ban dau
        hpCurrent = hpSlider.maxValue;
        manaCurrent = manaSlider.maxValue;

        //Hoi mau va mana tren giay
        InvokeRepeating("restorePerSecond", 0f, 1f);
    }
    public void Update()
    {
        //Effect mau va mana giam tu tu
        hpAndManaSmoothly();
    }
    public void hpAndManaSmoothly()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, hpCurrent, 7 * Time.deltaTime);
        manaSlider.value = Mathf.Lerp(manaSlider.value, manaCurrent, 7 * Time.deltaTime);
    }
    public void restorePerSecond()
    {
        if (hpCurrent >= hpSlider.maxValue) 
        { hpCurrent = hpSlider.maxValue; } 
        else 
        { hpCurrent += hpRestorePerSecond; }

        if (manaCurrent >= manaSlider.maxValue)
        { manaCurrent = manaSlider.maxValue; }
        else
        { manaCurrent += manaRestorePerSecond; }
    }
}
