using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    public PlayerMover player;
    private bool top;
    public Rigidbody2D rb;
    public bool CameraFlips = true;
    //private GameObject Camera;
    public bool isEnabled = false; // Use up arrow key to activate when enabled. For testing, will rewrite for triggers when needed.
    public bool hasFlippedDown = false;
    public bool hasFlippedUp = false;
    private bool once = true;
    private bool twice = true;
    private GameObject[] gravitems;

    public bool Top { get => top; set => top = value; }

    void Start()
    {
        //player = GetComponent<PlayerMover>();
        //rb = GetComponent<Rigidbody2D>();
        //Camera = GameObject.Find("CM vcam1");
        gravitems = GameObject.FindGameObjectsWithTag("GravityItem");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {

            if (hasFlippedDown && once == true)
            {
                rb.gravityScale *= -1;
                Rotation();

                for (int i = 0; i < gravitems.Length; i++)
                {
                    var rd = gravitems[i].GetComponent<Rigidbody2D>();
                    ObjectGravity.FlipObject(rd);
                }
                    hasFlippedDown = false;
                once = false;
                twice = true;
            }

            if (hasFlippedUp && twice == true)
            {
                rb.gravityScale *= -1;
                Rotation();
                for (int i = 0; i < gravitems.Length; i++)
                {
                    var rd = gravitems[i].GetComponent<Rigidbody2D>();
                    ObjectGravity.FlipObject(rd);
                }
                hasFlippedUp = false;
                twice = false;
                once = true;
            }
        }


    }
    void Rotation()
    {
        if (!Top)
        {
            //if camera flip is checked flip camera, if not ignore
            //if (CameraFlips)
            //    Camera.transform.eulerAngles = new Vector3(180f, 0, 0);
            player.transform.eulerAngles = new Vector3(0, 0, 180f);
            //flips the player when camera flips to maintain the correct direction
            player.Flip();
        }
        else
        {
            //if (CameraFlips)
            //Camera.transform.eulerAngles = Vector3.zero;
            player.transform.eulerAngles = Vector3.zero;
            player.Flip();
        }
        player.FacingRight = !player.FacingRight;
        Top = !Top;
    }
}
