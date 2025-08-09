using System.Collections;
using Game.Configs;
using NaughtyAttributes;
using UnityConstants;
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

        protected Tank Tank;
        private readonly YieldInstruction _waitForFixedUpdateInstruction = new WaitForFixedUpdate();
        private MonoBehaviour _coroutineRunner;
        private Coroutine _movementCoroutine;
        private Coroutine _rotationCoroutine;
        private Coroutine _collisionCoroutine;

        public bool IsActive { get; set; } = true;
        [ShowNativeProperty]
        public bool CollisionCoroutineActive => _collisionCoroutine != null;

        public void Initialize(MonoBehaviour coroutineRunner, Tank tank)
        {
            _coroutineRunner = coroutineRunner;
            Tank = tank;
        }

        public void LaunchPersistentMovement()
        {
            _movementCoroutine = _coroutineRunner.StartCoroutine(PersistentMovementCoroutine());
            _rotationCoroutine = _coroutineRunner.StartCoroutine(PersistentRotationCoroutine());
        }

        private void StopPersistentMovement()
        {
            _coroutineRunner.StopCoroutine(_movementCoroutine);
            _coroutineRunner.StopCoroutine(_rotationCoroutine);

            _movementCoroutine = null;
            _rotationCoroutine = null;
        }

        public void HandleCollision(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tag.Wall))
            {
                _collisionCoroutine ??= _coroutineRunner.StartCoroutine(CollisionCoroutine(180));
            }
            else if (collision.gameObject.layer == Layer.Tank)
            {
                _collisionCoroutine ??= _coroutineRunner.StartCoroutine(CollisionCoroutine(_config.RotationChangeDegrees));
            }
        }

        protected abstract IEnumerator ShootCoroutine();

        private IEnumerator PersistentMovementCoroutine()
        {
            while (IsActive)
            {
                Tank.Move(1);

                yield return _waitForFixedUpdateInstruction;
            }
        }

        private IEnumerator PersistentRotationCoroutine()
        {
            while (IsActive)
            {
                yield return new WaitForSeconds(_config.RotationChangeRandomInterval.RandomWithin);

                yield return ChangeRotationCoroutine(_config.RotationChangeDegrees);
            }
        }

        private IEnumerator ChangeRotationCoroutine(float rotationDegrees)
        {
            float sign = Mathf.Sign(Random.Range(-1, 1));
            float prevRotation = Tank.transform.rotation.eulerAngles.y;
            while (Mathf.Abs(Tank.transform.rotation.eulerAngles.y - prevRotation) < rotationDegrees)
            {
                Tank.Rotate(sign);

                yield return _waitForFixedUpdateInstruction;
            }
        }

        private IEnumerator CollisionCoroutine(float rotationDegrees)
        {
            StopPersistentMovement();

            yield return ChangeRotationCoroutine(rotationDegrees);

            LaunchPersistentMovement();

            _collisionCoroutine = null;
        }
    }
}