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
        private Coroutine _collisionCoroutine;

        public bool IsActive { get; set; } = true;
        [ShowNativeProperty]
        public bool CollisionCoroutineActive => _collisionCoroutine != null;

        public void Initialize(MonoBehaviour coroutineRunner, Tank tank)
        {
            _coroutineRunner = coroutineRunner;
            Tank = tank;
        }

        public void Launch()
        {
            _coroutineRunner.StartCoroutine(PersistentMovementCoroutine());
            _coroutineRunner.StartCoroutine(PersistentRotationCoroutine());
        }

        public void HandleCollision(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tag.Wall)
                || collision.gameObject.CompareTag(Tag.Player))
            {
                _collisionCoroutine = _coroutineRunner.StartCoroutine(CollisionCoroutine());
            }
        }

        protected abstract IEnumerator ShootCoroutine();

        private IEnumerator PersistentMovementCoroutine()
        {
            while (IsActive)
            {
                yield return new WaitUntil(() => _collisionCoroutine == null);

                Tank.Move(1);

                yield return _waitForFixedUpdateInstruction;
            }
        }

        private IEnumerator PersistentRotationCoroutine()
        {
            while (IsActive)
            {
                yield return new WaitUntil(() => _collisionCoroutine == null);

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

        private IEnumerator CollisionCoroutine()
        {
            yield return ChangeRotationCoroutine(180);

            _collisionCoroutine = null;
        }
    }
}