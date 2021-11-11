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
    public FlipGravity flipGrav;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;
    private bool touching = false;
    private int p;
    private float GravDefault = 1f; //default gravity is 1 for rigidbody
    public float Gravity = 1f; // Anything higher than 1 increases gravity. 0 is no gravity at all. Ex: 0.5 = half normal gravity
    public bool disableInput = false;
    //play animation
    public Animator animator;


    //colider?
    public Collider2D Player;
    public int Health = 3;
    private Rigidbody2D rb;
    public bool FacingRight { get => facingRight; set => facingRight = value; }


    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableInput == false)
        {
            //checks gravity value, if not's not equal to default, update it, then reassign the current gravity as new default.
            //In update method as gravity may be changed by triggers in-game.
            if (GravDefault != Gravity)
            {
                GravDefault = Gravity;
                rb.gravityScale = Gravity;
            }
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
                //Debug.Log("Touching ground");
                //if no longer touching world set touching to false and breaks out of if statement
                if (!Player.IsTouching(World[p].GetComponent<Collider2D>()))
                {
                    touching = false;
                }
            }
            //if not touching world run this
            else if (!touching)
            {
                //Debug.Log("Not touching ground");
            }
            //if I'm pressing jump button and touching then do this
            if (Input.GetKey(KeyCode.Space))
            {
                if (touching)
                {
                    //changes jumpforce depending on gravity.
                    if (flipGrav.Top)
                    {
                        rb.velocity = Vector2.up * jumpForce * -1;
                    }
                    else
                    {
                        rb.velocity = Vector2.up * jumpForce;
                    }

                }
            }
        }
       
    }






    private void FixedUpdate()
    {
        if (disableInput == false)
        {
            moveInput = Input.GetAxis("Horizontal");
            if (moveInput == 0)
                animator.SetBool("SpeedB", true);
            else
                animator.SetBool("SpeedB", false);
            //makes the player move
            if (!flipGrav.Top)
            {
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
            else
            {
                rb.velocity = new Vector2(moveInput * speed * -1, rb.velocity.y);
                if (FacingRight == false && moveInput < 0)
                {
                    Flip();
                }
                else if (FacingRight == true && moveInput > 0)
                {
                    Flip();
                }
            }
            
        }
        
    }


    public void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
