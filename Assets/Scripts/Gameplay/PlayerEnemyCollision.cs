using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerMover playerMover;
        public PlayerController playerController;
        bool willJumpHurtEnemy;
        bool willWeaponHurtEnemy;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            if (SceneManager.GetActiveScene().name == "Micro Scene")
            {
                willJumpHurtEnemy = playerController.Bounds.center.y >= enemy.Bounds.max.y;

                if (willJumpHurtEnemy)
                {
                    var enemyHealth = enemy.GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.Decrement();
                        if (!enemyHealth.IsAlive)
                        {
                            Schedule<EnemyDeath>().enemy = enemy;
                            playerController.Bounce(2);
                        }
                        else
                        {
                            playerController.Bounce(7);
                        }
                    }
                    else
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        playerController.Bounce(2);
                    }
                }
                else
                {
                    Schedule<PlayerDeath>();
                }
            }
            else if (SceneManager.GetActiveScene().name == "Connor's stuff")
            {
                willJumpHurtEnemy = playerMover.Bounds.center.y >= enemy.Bounds.max.y;
                willWeaponHurtEnemy = playerMover.WeaponCollider.isActiveAndEnabled && playerMover.WeaponCollider.IsTouching(enemy._collider);

                if (willJumpHurtEnemy || willWeaponHurtEnemy)
                {
                    var enemyHealth = enemy.GetComponent<Health>();
                    if (willJumpHurtEnemy)
                    {
                        if (enemyHealth != null)
                        {
                            enemyHealth.Decrement();
                            if (!enemyHealth.IsAlive)
                            {
                                Schedule<EnemyDeath>().enemy = enemy;
                                playerMover.Bounce(2);
                            }
                            else
                            {
                                playerMover.Bounce(7);
                            }
                        }
                        else
                        {
                            Schedule<EnemyDeath>().enemy = enemy;
                            playerMover.Bounce(2);
                        }

                    }
                    else if (willWeaponHurtEnemy)
                    {
                        if (enemyHealth != null)
                        {
                            enemyHealth.Decrement();
                            if (!enemyHealth.IsAlive)
                            {
                                Schedule<EnemyDeath>().enemy = enemy;
                            }
                        }
                        else
                        { //add check for player health <= 0
                            Schedule<EnemyDeath>().enemy = enemy;
                        }
                    }
                }
                else
                {
                    Schedule<PlayerDeath>();
                }

            }
        }
    }
}