using System.Collections;
using Evolutex.Evolunity.Extensions;
using UnityEngine;
using Zenject;

namespace Game
{
    public class AIPlayer : AbstractPlayer
    {
        [Inject]
        private AIBehaviour _behaviour;

        private IEnumerator Start()
        {
            _behaviour.Initialize(_tank);

            yield return _behaviour.BehaviourCoroutine();
        }

        public void RandomizePosition()
        {
            _tank.Respawn(GetRespawnPose());
        }

        protected override Pose GetRespawnPose()
        {
            return new Pose(
                Vector3.zero.Randomize(
                    (-GameConfig.BotSpawnLimits.x / 2, GameConfig.BotSpawnLimits.x / 2),
                    (0, 0),
                    (-GameConfig.BotSpawnLimits.y / 2, GameConfig.BotSpawnLimits.y / 2)),
                Quaternion.Euler(0, Random.Range(0, 360), 0));
        }

        protected override void HandleCollision(Collision collision)
        {
            _behaviour.HandleCollision(collision);
        }
    }
}