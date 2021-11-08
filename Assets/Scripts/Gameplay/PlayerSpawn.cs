using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            player.collider2d.enabled = true;
            player.controlEnabled = false;
            if (player.audioSource && player.respawnAudio)
                player.audioSource.PlayOneShot(player.respawnAudio);
            player.health.Increment();
            var otherPlayer = GameObject.Find("Actual player");
            if (otherPlayer != null)
            {
                var mySpawn = GameObject.Find("SpawnPoint");
                var spawnPoint = mySpawn.transform.position;
                float x = spawnPoint.x;
                float y = spawnPoint.y;
                player.transform.position = new Vector3(x,y);
                player.animator.SetBool("dead", false);
                model.virtualCamera.m_Follow = player.transform;
                model.virtualCamera.m_LookAt = player.transform;
                Simulation.Schedule<EnablePlayerInput>(2f);
            }
            else
            {
                player.Teleport(model.spawnPoint.transform.position);
                player.jumpState = PlayerController.JumpState.Grounded;
                player.animator.SetBool("dead", false);
                model.virtualCamera.m_Follow = player.transform;
                model.virtualCamera.m_LookAt = player.transform;
                Simulation.Schedule<EnablePlayerInput>(2f);
            }
            
        }
    }
}