using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Chest : MonoBehaviour
{
    public GameObject chestParent;
    public GameObject chestLocked;
    public GameObject chestOpened;
    public GameObject DamageText;
    public int DamageCurrent;
    public Slider HP;
    private int HPcurrent;
    public GameObject effects;
    public List<GameObject> Player;

    //Spawn Booster
    public List<GameObject> Booster;

    public GameObject countGetPoint;
    public void Start()
    {
        HPcurrent = (int)HP.value;
        DamageCurrent = 0;
        countGetPoint = GameObject.FindGameObjectWithTag("CountPoint");
    }
    public void Update()
    {
        //Effect mau giam tu tu
        if (HPcurrent > 0)
        {
            HP.value = Mathf.Lerp(HP.value, HPcurrent, 3 * Time.deltaTime);
        }
        else
        {
            HPcurrent = 1;
            HP.value = Mathf.Lerp(HP.value, 0f, 3 * Time.deltaTime);
            Invoke("openChest", 1.0f);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Spawn_Chest>().currentChestNum -= 1;
            countGetPoint.GetComponent<TextMeshProUGUI>().text = (int.Parse(countGetPoint.GetComponent<TextMeshProUGUI>().text) + 1).ToString();

            //Spawn Booster
            foreach (var item in this.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Spawn_Chest>().listLocationHadChest)
            {
                if(this.gameObject.transform.position == item.transform.position)
                {
                    int i = (int)Random.Range(0f, 7f);
                    Instantiate(Booster[i], this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform);
                    this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.childCount - 1).gameObject.transform.position = item.gameObject.transform.position;
                    this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.childCount - 1).gameObject.layer = item.transform.parent.gameObject.layer;
                    this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.childCount - 1).gameObject.GetComponent<SortingGroup>().sortingOrder = item.transform.parent.GetComponent<TilemapRenderer>().sortingOrder;
                    break;
                }
            }
        }

        //Tuong tac
        foreach (var item in Player)
        {
            if (item.GetComponent<scr>().isAttacking)
            {
                item.GetComponent<scr>().isAttacking = false;
                //Make effects
                int randomEffect = (int)Random.Range(3f, 6f);
                effects.GetComponent<Animator>().SetInteger("Execute", randomEffect);
                this.gameObject.GetComponent<Animator>().SetBool("Execute", true);
                Invoke("return0", 0.1f);

                //Set damage
                DamageCurrent = (int)Random.Range(item.GetComponent<scr>().damage - 5, item.GetComponent<scr>().damage + 5);

                //Subtraction HP of chest
                HPcurrent -= DamageCurrent;

                //Subtraction HP of player
                //item.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Slider>().value -= DamageCurrent * 0.1f;
                item.GetComponent<scr>().HpCurrent -= DamageCurrent * 0.1f;

                //Call effects text damage
                DamageText.GetComponent<Spawn>().spawnObjectDamageText(DamageCurrent);

                //HP line auto hide after 2 secon
                HP.gameObject.SetActive(true);
                StartCoroutine(checkDamageContinue());
            }
        }
    }
    IEnumerator checkDamageContinue()
    {
        float hp = HPcurrent;
        yield return new WaitForSecondsRealtime(2f);
        if(hp == HPcurrent) { HP.gameObject.SetActive(false); }
        yield break;
    }
    private void return0()
    {
        effects.GetComponent<Animator>().SetInteger("Execute", 0);
        this.gameObject.GetComponent<Animator>().SetBool("Execute", false);
    }
    private void openChest()
    {
        chestParent.GetComponent<Spawn>().spawnObjectSimple(1);
        float i = (int)Random.Range(0f,2f);
        if (i < 1f)
        {
            chestParent.GetComponent<Spawn>().spawnObjectSimple(2);
        }
        else
        {
            chestParent.GetComponent<Spawn>().spawnObjectSimple(3);
        }
        HP.transform.parent.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
    public void Burning()
    {
        foreach (var item in Player)
        {
            item.gameObject.GetComponent<scr>().HpCurrent -= 10f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.Remove(collision.gameObject);
    }
}