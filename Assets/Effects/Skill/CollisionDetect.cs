using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public int Damage;
    public GameObject damageText;
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            int damage = Random.Range(Damage - 2, Damage + 2);
            other.GetComponent<scr>().HpCurrent -= damage;
            damageText.GetComponent<Spawn>().spawnObjectDamageText(damage);
            //damageText.transform.GetChild(damageText.transform.childCount - 1).gameObject.transform.position = other.collider.particleSystem.GetParticles
        }
    }
}
