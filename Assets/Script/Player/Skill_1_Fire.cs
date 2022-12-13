using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Skill_1_Fire : MonoBehaviour
{
    public GameObject skill_L2, skill_L5, parent, Location;
    public float subtractMana;
    public float delayTime;
    private float cout;
    public bool canUse;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && transform.GetComponent<Core>().manaCurrent >= subtractMana && canUse)
        {
            //Spawn skill
            if (this.gameObject.layer == 7)
            {
                Instantiate(skill_L2, parent.transform);
            }
            else
            {
                Instantiate(skill_L5, parent.transform);
            }
            GameObject skillSpawned = parent.transform.GetChild(parent.transform.childCount - 1).gameObject;
            skillSpawned.transform.position = Location.transform.position;
            skillSpawned.transform.rotation = Quaternion.Euler(0f, 0f, (float)Location.transform.parent.rotation.eulerAngles.z);
            skillSpawned.gameObject.layer = this.gameObject.layer;
            skillSpawned.transform.GetChild(0).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.gameObject.GetComponent<SortingGroup>().sortingOrder;

            //Subtract mana
            transform.GetComponent<Core>().manaCurrent -= subtractMana;

            //Delay skill
            canUse = false;
        }
        else if(cout <= 0f)
        {
            cout = delayTime;
            canUse = true;
        }
        else if(canUse == false)
        {
            cout -= Time.deltaTime;
        }
    }
}
