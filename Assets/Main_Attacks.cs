using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Attacks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int Space = (int)Random.Range(1, 3);
            this.gameObject.GetComponent<Animator>().SetInteger("Execute", Space);
            Invoke("return0", 0.1f);
        }
    }
    public void return0()
    {
        this.gameObject.GetComponent<Animator>().SetInteger("Execute", 0);
    }
}
