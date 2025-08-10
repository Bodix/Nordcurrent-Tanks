using Evolutex.Evolunity.Extensions;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Game
{
    public class AIPlayer : AbstractPlayer
    {
        [Inject, ShowNonSerializedField]
        private AIBehaviour _behaviour;

        protected override void Awake()
        {
            base.Awake();

            _behaviour.Initialize(this, _tank);
        }

        private void Start()
        {
            _behaviour.LaunchPersistentMovement();
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

        protected override void HandleCollisionEnter(Collision collision)
        {
            _behaviour.HandleCollisionEnter(collision);
        }

        protected override void HandleCollisionExit(Collision collision)
        {
            _behaviour.HandleCollisionExit(collision);
        }
    }
}