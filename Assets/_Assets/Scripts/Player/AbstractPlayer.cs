using Evolutex.Evolunity.Utilities;
using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public abstract class AbstractPlayer : MonoBehaviour
    {
        [SerializeField]
        protected Tank _tank;

        [Inject]
        protected GameConfig GameConfig;

        protected virtual void Awake()
        {
            _tank.Collided += HandleCollision;
            _tank.Killed += DelayedRespawn;
        }

        protected abstract Pose GetRespawnPose();
        protected abstract void HandleCollision(Collision collision);

        private void DelayedRespawn()
        {
            Delay.ForSeconds(GameConfig.RespawnDelay, () => _tank.Respawn(GetRespawnPose()), this);
        }
    }
}