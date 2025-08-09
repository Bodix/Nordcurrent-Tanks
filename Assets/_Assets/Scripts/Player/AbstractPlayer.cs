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
            _tank.CollisionEnter += HandleCollisionEnter;
            _tank.CollisionExit += HandleCollisionExit;
            _tank.Killed += DelayedRespawn;
        }


        protected abstract Pose GetRespawnPose();
        protected abstract void HandleCollisionEnter(Collision collision);
        protected abstract void HandleCollisionExit(Collision collision);

        private void DelayedRespawn()
        {
            Delay.ForSeconds(GameConfig.RespawnDelay, () => _tank.Respawn(GetRespawnPose()), this);
        }
    }
}