using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFollow : MonoBehaviour
{
    public GameObject Follower;
    public GameObject Target;
    Vector2 velocity = Vector2.zero;
    public float speed = 1f;
    public int RocketFlyMode = 0;

    void Update()
    {
        //rocket fly to the target
        mode(RocketFlyMode);
        Follower.transform.up = Target.transform.position - Follower.transform.position;

        //target fly to the mouse
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.transform.position = new Vector3(MousePosition.x, MousePosition.y, 10);
    }
    public void mode(int m)
    {
        switch (m)
        {
            case 0:
                {
                    Follower.transform.position = Vector2.MoveTowards(Follower.transform.position, Target.transform.position, speed * 400f * Time.deltaTime);
                    break;
                }
            case 1:
                {
                    Follower.transform.position = Vector2.Lerp(Follower.transform.position, Target.transform.position, speed * 3f * Time.deltaTime);
                    break;
                }
            case 2:
                {
                    Follower.transform.position = Vector2.SmoothDamp(Follower.transform.position, Target.transform.position, ref velocity, (90f - (speed * 15)) * Time.deltaTime);
                    break;
                }
        }
    }
}
