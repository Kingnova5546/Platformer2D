using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        private GameObject playerObject;
        public override void Execute()
        {
            var user = GameObject.Find("Actual player");
            if (user != null)
            {
                var health = user.GetComponent<PlayerMover>();
                Debug.Log(health.Health);
            }
            playerObject = GameObject.Find("Fake Player");
            var GameController = GameObject.Find("GameController");
           
            var player = model.player; 
            if (player.health.IsAlive)
            {
                player.health.Die();

                //if (playerObject == null)
                //{
                //    var Anim = GameObject.Find("CM Vcam1");
                //    var AnimComp = Anim.GetComponent<Animator>();
                //    AnimComp.gameObject.SetActive(false);
                //}
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                // player.collider.enabled = false;
                player.controlEnabled = false;
                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                player.animator.SetBool("dead", true);
                Simulation.Schedule<PlayerSpawn>(2);
                if (playerObject != null)
                {
                    var check = playerObject.GetComponent<Platformer.Mechanics.PlayerController>();
                    if (check.isCursed)
                    {
                        Debug.Log("Died while cursed check passed! Need to put scene change logic HERE.");
                        GameObject.Destroy(GameController);
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }
    }
}