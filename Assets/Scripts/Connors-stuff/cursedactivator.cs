using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

public class cursedactivator : MonoBehaviour
{
    public GameObject cursedtext;
    public GameObject cursedtoken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (cursedtoken != null)
        {
            player.animator.SetTrigger("hurt");
            player.audioSource.PlayOneShot(player.ouchAudio);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
