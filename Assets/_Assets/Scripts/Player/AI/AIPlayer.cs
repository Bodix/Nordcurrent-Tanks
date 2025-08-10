using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Game
{
    [SelectionBase]
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
            _behaviour.LaunchPersistentBehaviour();
        }

        public void RandomizePosition()
        {
            _tank.Respawn(GetRespawnPose());
        }

        protected override Pose GetRespawnPose()
        {
            float halfX = GameConfig.BotSpawnLimits.x / 2f;
            float halfZ = GameConfig.BotSpawnLimits.y / 2f;

            Vector3 pos = Random.value < 0.5f
                ? new Vector3(Random.value < 0.5f ? -halfX : halfX, 0, Random.Range(-halfZ, halfZ))
                : new Vector3(Random.Range(-halfX, halfX), 0, Random.value < 0.5f ? -halfZ : halfZ);

            return new Pose(pos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        }

        // This method spawns bots across the entire area of the rectangle,
        // instead of spawning them only along the perimeter (as stated in the task).
        //
        // protected override Pose GetRespawnPose()
        // {
        //     return new Pose(
        //         Vector3.zero.Randomize(
        //             (-GameConfig.BotSpawnLimits.x / 2, GameConfig.BotSpawnLimits.x / 2),
        //             (0, 0),
        //             (-GameConfig.BotSpawnLimits.y / 2, GameConfig.BotSpawnLimits.y / 2)),
        //         Quaternion.Euler(0, Random.Range(0, 360), 0));
        // }

        protected override void HandleCollision(Collision collision)
        {
            _behaviour.HandleCollision(collision);
        }
    }
}