using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zone : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 15);
        Physics2D.IgnoreLayerCollision(15, 7);
        Physics2D.IgnoreLayerCollision(15, 8);
        Physics2D.IgnoreLayerCollision(15, 9);
        Physics2D.IgnoreLayerCollision(15, 10);
        Physics2D.IgnoreLayerCollision(15, 11);
        Physics2D.IgnoreLayerCollision(15, 12);
        Physics2D.IgnoreLayerCollision(15, 13);
        Physics2D.IgnoreLayerCollision(15, 14);
    }
}
