using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    public static ObjectGravity instance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    void Update()
    {
        rb = rb;
    }

    public void FlipObject()
    {
        rb.gravityScale *= -1;
    }
}
