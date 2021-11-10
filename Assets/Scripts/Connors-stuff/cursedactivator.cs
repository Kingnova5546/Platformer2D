using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

public class cursedactivator : MonoBehaviour
{
    public GameObject CursedText;
    public GameObject music;
    public bool isCursed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        var musicAn = music.GetComponent<Animator>();
        var cursedAn = CursedText.GetComponent<Animator>();
        var player = other.gameObject.GetComponent<PlayerMover>();
            player.animator.SetTrigger("hurt");
            player.audioSource.PlayOneShot(player.ouchAudio);
        musicAn.SetBool("canChange", true);
        cursedAn.SetBool("canChange", true);
        isCursed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
