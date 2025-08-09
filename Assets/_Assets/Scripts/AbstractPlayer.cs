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
            _tank.Destroyed += DisableTemporarily;
        }

        public void Respawn()
        {
            Pose pose = GetRespawnPose();

            _tank.transform.position = pose.position;
            _tank.transform.rotation = pose.rotation;

            _tank.gameObject.SetActive(true);
        }

        protected abstract Pose GetRespawnPose();

        private void DisableTemporarily()
        {
            _tank.gameObject.SetActive(false);

            Delay.ForSeconds(GameConfig.RespawnDelay, Respawn, this);
        }
    }
}