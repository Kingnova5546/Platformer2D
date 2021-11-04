using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private PlayerMover player;
    private bool top;
    private Rigidbody2D rb;
    public bool CameraFlips = true;
    private GameObject Camera;
    public bool isEnabled = false; // Use up arrow key to activate when enabled. For testing, will rewrite for triggers when needed.

    public bool Top { get => top; set => top = value; }

    void Start()
    {
        player = GetComponent<PlayerMover>();
        rb = GetComponent<Rigidbody2D>();
        Camera = GameObject.Find("CM vcam1");
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.gravityScale *= -1;
                Rotation();
            }
        }
       

    }
    void Rotation()
    {
        if (!Top)
        {
            //if camera flip is checked flip camera, if not ignore
            if (CameraFlips)
            Camera.transform.eulerAngles = new Vector3(180f, 0, 0);
            transform.eulerAngles = new Vector3(0, 0, 180f);
            //flips the player when camera flips to maintain the correct direction
            player.Flip();
        }
        else
        {
            if (CameraFlips)
            Camera.transform.eulerAngles = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            player.Flip();
        }
        player.FacingRight = !player.FacingRight;
        Top = !Top;
    }
}
