using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class scr : MonoBehaviour
{
    public float speed = 1f;
    public GameObject Canvas_HP_Mana;
    public GameObject Attack;
    public float TimeAttackDelay;
    public float ManaLostPerHit;
    private bool canAttack;
    public bool isAttacking;
    public Slider HP;
    public Slider Mana;
    public float damage;
    public float HpCurrent;
    private Vector2 newPosition;
    public Camera mainCamera;
    public float HP_Restore_per_Second;
    public float Mana_Restore_per_Second;
    private void Awake()
    {
        canAttack = true;
        HpCurrent = HP.maxValue;
    }
    private void Start()
    {
        //Restore HP and MANA 1 unit per secon
        InvokeRepeating("HPrestore", 0f, 1f);
        InvokeRepeating("MANArestore", 0f, 1f);
    }

    void Update()
    {
        //Giam mau tu tu va hoi sinh
        if(HpCurrent >= HP.maxValue)
        {
            HpCurrent = HP.maxValue;
        }
        else if (HpCurrent > 0f) 
        {
            //StopCoroutine(rebornPlayer());
            HP.value = Mathf.Lerp(HP.value, HpCurrent, 3 * Time.deltaTime); 
        }
        else if (HpCurrent <= 0f)
        {
            CancelInvoke();
            HpCurrent = 0f;
            HP.value = Mathf.Lerp(HP.value, 0f, 3 * Time.deltaTime);
            Time.timeScale = Mathf.MoveTowards(1f, 0f, 3 * Time.deltaTime);
            //Debug.Log(mainCamera.transform.GetChild(1).name);
            Color a = mainCamera.transform.GetChild(1).GetComponentInChildren<SpriteRenderer>().material.color;
            a.a = Mathf.MoveTowards(0f, 1f, 2 * Time.deltaTime);
            Debug.Log("Bright down!!!");
            //Screen.brightness = Mathf.MoveTowards(1f, 0f, 1 * Time.deltaTime);
            //StartCoroutine(rebornPlayer());
        }

        //Change sortingOrder to alway sync to main player
        Canvas_HP_Mana.GetComponent<Canvas>().sortingOrder = this.gameObject.GetComponent<SortingGroup>().sortingOrder + 1;

        //Main control
        Physics2D.IgnoreLayerCollision(10, 7);
        Physics2D.IgnoreLayerCollision(10, 8);
        if (Input.GetKey(KeyCode.A))
        {
            newPosition = new Vector2(this.gameObject.transform.position.x - speed, this.gameObject.transform.position.y);
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, newPosition, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition = new Vector2(this.gameObject.transform.position.x + speed, this.gameObject.transform.position.y);
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, newPosition, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - speed);
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, newPosition, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            newPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + speed);
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, newPosition, speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canAttack && Mana.value > ManaLostPerHit)
        {
            canAttack = false;
            isAttacking = true;
            Invoke("Delay", TimeAttackDelay);
            int Space = (int)Random.Range(1, 3);
            Attack.GetComponent<Animator>().SetInteger("Execute", Space);
            Invoke("return0", 0.1f);
            Mana.value -= ManaLostPerHit;
        }
    }
    IEnumerator rebornPlayer()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = Mathf.MoveTowards(1f, 0f, 3 * Time.deltaTime);

    }
    private void Delay()
    {
        canAttack = true;
    }
    private void HPrestore()
    {
        HpCurrent += HP_Restore_per_Second;
    }
    private void MANArestore()
    {
        Mana.value += Mana_Restore_per_Second;
    }
    private void return0()
    {
        Attack.GetComponent<Animator>().SetInteger("Execute", 0);
        isAttacking = false;
    }
}
