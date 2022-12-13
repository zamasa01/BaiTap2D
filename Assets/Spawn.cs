using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawn : MonoBehaviour
{
    public GameObject[] objToSpawn;
    public bool isAutoDestroy;
    private void Start()
    {
        isAutoDestroy = true;
    }
    private void Update()
    {
        if (objToSpawn[0] == null && isAutoDestroy) { isAutoDestroy = false; StartCoroutine(autoDestroy()); }
    }
    public void spawnObjectWithEffects(float time)
    {
        Instantiate(objToSpawn[0], gameObject.transform);
        Invoke("wait", time);
    }
    public void spawnObjectSimple(int a)
    {
        Instantiate(objToSpawn[a], gameObject.transform);
    }
    public void spawnObjectDamageText(int damg)
    {
        Instantiate(objToSpawn[0], gameObject.transform);
        this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = damg.ToString();
        this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.transform.GetChild(0).gameObject.layer = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.layer;
        this.gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<Canvas>().sortingOrder = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<SortingGroup>().sortingOrder;
    }
    private void wait()
    {
        Instantiate(objToSpawn[1], gameObject.transform);
    }
    public IEnumerator autoDestroy()
    {
        yield return new WaitForSecondsRealtime(14f);
        foreach (var item in this.gameObject.transform.parent.GetComponent<Spawn_Chest>().listLocationHadChest)
        {
            if(this.gameObject.transform.position == item.gameObject.transform.position)
            {
                this.gameObject.transform.parent.GetComponent<Spawn_Chest>().listLocation.Add(item);
                this.gameObject.transform.parent.GetComponent<Spawn_Chest>().listLocationHadChest.Remove(item);
                Vector3 temp = item.transform.position;
                item.transform.position = new Vector3(-20f, -20f, 0f);
                yield return new WaitForSecondsRealtime(0.01f);
                item.transform.position = temp;
                break;
            }
        }
        this.gameObject.transform.GetChild(2).gameObject.GetComponent<Burn>().stopBurn();
        yield return new WaitForSecondsRealtime(1f);
        Destroy(this.gameObject);
        yield break;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        spawnObjectWithEffects(1.0f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
