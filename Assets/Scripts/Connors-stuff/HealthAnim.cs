using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAnim : MonoBehaviour
{
    public PlayerMover health;
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (health.Health >= 3)
        //{
            
        //}
        if (health.Health == 2)
        {
            Anim.SetBool("Reset", false);
            Anim.SetTrigger("Heart1");
        }
        else if (health.Health == 1)
        {
            Anim.SetTrigger("Heart2");
            Anim.SetTrigger("Heart3");
        }
       if (health.Health <= 0)
        {
            Anim.ResetTrigger("Heart1");
            Anim.ResetTrigger("Heart2");
            Anim.ResetTrigger("Heart3");
            Anim.SetBool("Reset", true);
            health.Health = 3;
        }
        
    }
}
