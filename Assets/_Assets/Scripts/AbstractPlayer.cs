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
            _tank.Destroyed += DelayedRespawn;
        }

        protected abstract Pose GetRespawnPose();

        private void DelayedRespawn()
        {
            Delay.ForSeconds(GameConfig.RespawnDelay, () => _tank.Respawn(GetRespawnPose()), this);
        }
    }
}