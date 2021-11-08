using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {

    }

    public static void FlipObject(Rigidbody2D rb)
    {
        rb.gravityScale *= -1;
    }
}
