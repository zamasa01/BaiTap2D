using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float Bx, By, x, y;
    public GameObject up,down,left, right;
    public GameObject AxisSpawnSkill;
    public float speedRun;
    public bool isTouchWall = false;
    private void Start()
    {
        //Bat dau ham check vi tri
        InvokeRepeating("check", 0f, 0.1f);
    }
    private void Update()
    {
        //Toc do Animation
        transform.GetChild(0).GetComponent<Animator>().SetFloat("MoveSpeed", speedRun/2);

        //Dieu khien nhan vat
        float MoveX = transform.position.x;
        float MoveY = transform.position.y;
        float MoveZ = transform.position.z;
        if (Input.GetKey(KeyCode.W) && !up.GetComponent<CheckWay>().onTouch) { MoveY += (speedRun * Time.deltaTime); }
        if (Input.GetKey(KeyCode.S) && !down.GetComponent<CheckWay>().onTouch) { MoveY -= (speedRun * Time.deltaTime); }
        if (Input.GetKey(KeyCode.A) && !left.GetComponent<CheckWay>().onTouch) { MoveX -= (speedRun * Time.deltaTime); }
        if (Input.GetKey(KeyCode.D) && !right.GetComponent<CheckWay>().onTouch) { MoveX += (speedRun * Time.deltaTime); }
        transform.position = new Vector3(MoveX, MoveY, MoveZ);

        //Lay so lieu
        Bx = transform.position.x;
        By = transform.position.y;
        int animatorNum = transform.GetChild(0).GetComponent<Animator>().GetInteger("Control-Int");

        //Huong quay va chay cua nhan vat
        if (Bx == x && By == y && animatorNum%2 == 0) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", ((int)animatorNum - 1)); }
        else if(Bx == x && By < y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 2); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
        else if (Bx > x && By < y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 4); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 45f); }
        else if (Bx > x && By == y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 6); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 90f); }
        else if (Bx > x && By > y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 8); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 135f); }
        else if (Bx == x && By > y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 10); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 180f); }
        else if (Bx < x && By > y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 12); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 225f); }
        else if (Bx < x && By == y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 14); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 270f); }
        else if (Bx < x && By < y) { transform.GetChild(0).GetComponent<Animator>().SetInteger("Control-Int", 16); AxisSpawnSkill.transform.rotation = Quaternion.Euler(0f, 0f, 315f); }
    }
    //Ham check vi tri
    public void check()
    {
        x = transform.position.x;
        y = transform.position.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchWall = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchWall = false;
    }
}
