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
        var World = GameObject.FindGameObjectsWithTag("World");/*.GetComponents<Collider2D>();*/

        for (int i = 0; i < World.Length; i++)
        {
            if (Player.IsTouching(World[i].GetComponent<Collider2D>()))
            {
                Debug.Log("You are touching ground");

            }
            else
            {
                Debug.Log("You aren't touching ground");
            }
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
        else if (facingRight == true && moveInput <0)
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
