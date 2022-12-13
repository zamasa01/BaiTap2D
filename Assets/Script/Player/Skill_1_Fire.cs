using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Skill_1_Fire : MonoBehaviour
{
    public GameObject skill, parent, Location;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(skill, parent.transform);
            GameObject skillSpawned = parent.transform.GetChild(parent.transform.childCount - 1).gameObject;
            skillSpawned.transform.position = Location.transform.position;
            skillSpawned.transform.rotation = Quaternion.Euler(0f, 0f, (float)Location.transform.parent.rotation.eulerAngles.z);
            skillSpawned.gameObject.layer = this.gameObject.layer;
            skillSpawned.transform.GetChild(0).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.gameObject.GetComponent<SortingGroup>().sortingOrder;
        }
    }
}
