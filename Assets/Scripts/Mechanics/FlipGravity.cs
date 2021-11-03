using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private PlayerMover player;
    private bool top;
    private Rigidbody2D rb;
    private float GravDefault = 1f;
    public float Gravity = 1f;
    public bool CameraFlips = true;
    private GameObject Camera;

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
        if(GravDefault != Gravity)
        {
            GravDefault = Gravity;
            rb.gravityScale = Gravity;
        }
        if (Input.GetKeyDown(KeyCode.Space))
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
