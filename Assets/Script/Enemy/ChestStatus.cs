using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestStatus : MonoBehaviour
{
    public Slider hpSlider;
    public Slider manaSlider;
    private void Start()
    {
        transform.GetComponent<Core>().manaCurrent = 0f;
        transform.GetComponent<Core>().manaRestorePerSecond = -1;

        hpSlider = transform.GetComponent<Core>().hpSlider;
        manaSlider = transform.GetComponent<Core>().manaSlider;
    }
    private void Update()
    {
        
    }
}
