using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private PlayerMover player;
    private bool top;

    private Rigidbody2D rb;

    public bool Top { get => top; set => top = value; }

    void Start()
    {
        player = GetComponent<PlayerMover>();
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Is this working?");
            rb.gravityScale *= -1;
            Rotation();
        }
    }
    void Rotation()
    {
        if (!Top)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        player.FacingRight = !player.FacingRight;
        Top = !Top;
    }
}
