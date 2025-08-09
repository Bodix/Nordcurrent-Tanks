using System.Collections;
using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    // Use this CreateAssetMenu pattern for every new implementation of AIBehaviour.
    // [CreateAssetMenu(fileName = nameof(ClassName), menuName = "Game/AI Behaviours/" + nameof(ClassName), order = 0)]
    public abstract class AIBehaviour : ScriptableObject
    {
        [Inject]
        private AIConfig _config;

        private readonly YieldInstruction _waitForFixedUpdateInstruction = new WaitForFixedUpdate();
        private Tank _tank;
        private float _elapsedRotateChangeTime;
        // private float _rotateC

        public bool IsActive { get; set; } = true;

        public void Initialize(Tank tank)
        {
            _tank = tank;
        }

        public void Launch(MonoBehaviour coroutineRunner)
        {
            coroutineRunner.StartCoroutine(MovementCoroutine());
            coroutineRunner.StartCoroutine(RotationCoroutine());
        }

        public void HandleCollision(Collision collision)
        {
            if (collision.gameObject.layer == UnityConstants.Layer.Ground)
            {
            }
        }

        private IEnumerator MovementCoroutine()
        {
            while (IsActive)
            {
                yield return _waitForFixedUpdateInstruction;

                _tank.Move(1);
            }
        }

        private IEnumerator RotationCoroutine()
        {
            while (IsActive)
            {
                yield return new WaitForSeconds(_config.RandomRotationInterval.RandomWithin);

                _tank.Rotate(1);
            }
        }
    }
}