using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_CollisionDetect : MonoBehaviour
{
    public int Damage,sumDamage;
    public GameObject damageTextEffect;
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            int damage = Random.Range(Damage - 2, Damage + 2);
            other.GetComponent<Core>().hpCurrent -= damage;
            GameObject TextSpawn = Instantiate(damageTextEffect, other.transform);
            TextSpawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = damage.ToString();
            TextSpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
            float otherX = other.transform.position.x;
            float otherY = other.transform.position.y;
            TextSpawn.transform.position = new Vector2(Random.Range(otherX - 0.5f, otherX + 0.5f), Random.Range(otherY - 0.5f, otherY + 0.5f));

            //Test
            sumDamage += damage;
        }
    }
}
