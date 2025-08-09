using Evolutex.Evolunity.Extensions;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : AbstractPlayer
    {
        [SerializeField]
        private Transform[] _spawnPoints;

        [Inject]
        private InputReader _inputReader;

        protected override void Awake()
        {
            base.Awake();

            _inputReader.MoveInput += _tank.HandleInput;
            _inputReader.FireInput += _tank.Shoot;
        }

        private void OnDestroy()
        {
            _inputReader.MoveInput -= _tank.HandleInput;
            _inputReader.FireInput -= _tank.Shoot;
        }

        protected override Pose GetRespawnPose()
        {
            return _spawnPoints.Random().GetPose();
        }

        protected override void HandleCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<Tank>())
                _tank.Kill();
        }

        protected override void HandleCollisionExit(Collision collision)
        {
        }
    }
}