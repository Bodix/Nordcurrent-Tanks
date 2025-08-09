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
                yield return new WaitForSeconds(_config.RotationChangeRandomInterval.RandomWithin);
                yield return ChangeRotationCoroutine();
            }
        }

        private IEnumerator ChangeRotationCoroutine()
        {
            float sign = Mathf.Sign(Random.Range(-1, 1));
            float prevRotation = _tank.transform.rotation.eulerAngles.y;
            while (Mathf.Abs(_tank.transform.rotation.eulerAngles.y - prevRotation) < _config.RotationChangeDegrees)
            {
                _tank.Rotate(sign);

                yield return _waitForFixedUpdateInstruction;
            }
        }
    }
}