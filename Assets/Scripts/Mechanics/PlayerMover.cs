using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using static Platformer.Mechanics.PlayerController;

public class PlayerMover : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;
    private bool touching = false;
    private int p;

    //colider?
    public Collider2D Player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        //collider2d = GetComponent<Collider2D>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Find all objects with tag the tag world
        var World = GameObject.FindGameObjectsWithTag("World");
        //loop through each object with tag world
        for (int i = 0; i < World.Length; i++)
        {
            //loops through each componenet in the selected object (object selected by i)
            if (Player.IsTouching(World[i].GetComponent<Collider2D>()))
            {
                touching = true;
                p = i;
                break;
            }
        }
        //if touching world run this
        if (touching)
        {
            Debug.Log("Touching ground");
            //if no longer touching world set touching to false and breaks out of if statement
            if (!Player.IsTouching(World[p].GetComponent<Collider2D>()))
            {
                touching = false;
            }
        }
        else if (!touching)
        {
            Debug.Log("Not touching ground");
        }

    }






    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
