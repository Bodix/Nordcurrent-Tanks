using System;
using System.Collections;
using System.Collections.Generic;
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
        private List<Collision> _currentCollisions;

        private void Awake()
        {
            _currentCollisions = new List<Collision>();
        }

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

        public void HandleCollisionEnter(Collision collision)
        {
            _currentCollisions.Add(collision);

            if (collision.gameObject.CompareTag(Tag.Wall))
            {
                _collisionCoroutine ??= _coroutineRunner.StartCoroutine(CollisionCoroutine(180));
            }
            else if (collision.gameObject.layer == Layer.Tank)
            {
                _collisionCoroutine ??= _coroutineRunner.StartCoroutine(CollisionCoroutine(_config.RotationChangeDegrees));
            }
        }

        public void HandleCollisionExit(Collision collision)
        {
            _currentCollisions.Remove(collision);
        }

        protected abstract IEnumerator ShootCoroutine();

        private void StopPersistentMovement()
        {
            _coroutineRunner.StopCoroutine(_movementCoroutine);
            _coroutineRunner.StopCoroutine(_rotationCoroutine);

            _movementCoroutine = null;
            _rotationCoroutine = null;
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
            float sign = Mathf.Sign(UnityEngine.Random.Range(-1, 1));
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