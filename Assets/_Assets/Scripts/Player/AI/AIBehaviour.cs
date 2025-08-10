using System.Collections;
using Evolutex.Evolunity.Structs;
using Game.Configs;
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
        private Coroutine _shootingCoroutine;
        private Coroutine _collisionCoroutine;

        public bool IsActive { get; set; } = true;

        public void Initialize(MonoBehaviour coroutineRunner, Tank tank)
        {
            _coroutineRunner = coroutineRunner;
            Tank = tank;
        }

        public void LaunchPersistentBehaviour()
        {
            _movementCoroutine = _coroutineRunner.StartCoroutine(PersistentMovementCoroutine());
            _rotationCoroutine = _coroutineRunner.StartCoroutine(PersistentRotationCoroutine());
            _shootingCoroutine = _coroutineRunner.StartCoroutine(ShootingCoroutine());
        }

        public void HandleCollision(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tag.Wall))
            {
                _collisionCoroutine ??= _coroutineRunner.StartCoroutine(CollisionCoroutine(180 - new FloatRange(1, 45).RandomWithin));
            }
            else if (collision.gameObject.layer == Layer.Tank)
            {
                _collisionCoroutine ??= _coroutineRunner.StartCoroutine(CollisionCoroutine(_config.RotationChangeDegrees));
            }
        }

        protected abstract IEnumerator ShootingCoroutine();

        private void StopPersistentBehaviour()
        {
            _coroutineRunner.StopCoroutine(_movementCoroutine);
            _coroutineRunner.StopCoroutine(_rotationCoroutine);
            _coroutineRunner.StopCoroutine(_shootingCoroutine);

            _movementCoroutine = null;
            _rotationCoroutine = null;
            _shootingCoroutine = null;
        }

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
            Quaternion prevRotation = Tank.transform.rotation;

            while (Quaternion.Angle(prevRotation, Tank.transform.rotation) < rotationDegrees)
            {
                Tank.Rotate(sign);

                yield return _waitForFixedUpdateInstruction;
            }
        }

        private IEnumerator CollisionCoroutine(float rotationDegrees)
        {
            StopPersistentBehaviour();

            yield return ChangeRotationCoroutine(rotationDegrees);

            LaunchPersistentBehaviour();

            _collisionCoroutine = null;
        }
    }
}