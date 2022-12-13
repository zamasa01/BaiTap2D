using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliderLayer : MonoBehaviour
{
    public List<int> Layer;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(Layer[0], Layer[1]);
        Physics2D.IgnoreLayerCollision(Layer[0], Layer[2]);
    }
}
