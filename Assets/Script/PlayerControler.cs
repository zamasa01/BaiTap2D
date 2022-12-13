using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControler : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 Move = new Vector3(100f,0f,0f);
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Crouch
        if (Input.GetKey(KeyCode.C))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Crouch_On");
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Crouch_Off");
        }

        //Run
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0f, 0);
            this.transform.position = Vector2.Lerp(this.transform.position, this.transform.position + Move, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
            this.transform.position = Vector2.Lerp(this.transform.position, this.transform.position - Move, speed * Time.deltaTime);
        }

        this.gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed));
    }
}
