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
    private FlipGravity flipGrav;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;
    private bool touching = false;
    private int p;

    //colider?
    public Collider2D Player;
    private Rigidbody2D rb;

    public bool FacingRight { get => facingRight; set => facingRight = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        //collider2d = GetComponent<Collider2D>();
    }
    void Start()
    {
        flipGrav = GetComponent<FlipGravity>();
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
        //if not touching world run this
        else if (!touching)
        {
            Debug.Log("Not touching ground");
        }
        //if I'm pressing jump button and touching then do this
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (touching)
            {
                //changes jumpforce depending on gravity.
                if(flipGrav.Top)
                rb.velocity = Vector2.up * jumpForce * -1;
                else
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (touching)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }

    }






    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (FacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (FacingRight == true && moveInput < 0)
        {
            Flip();
        }
    }


    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
