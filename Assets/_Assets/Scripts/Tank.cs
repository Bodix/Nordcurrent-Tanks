using System;
using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Tank : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private Transform _muzzleTransform;

        [Inject]
        private TankConfig _tankConfig;
        [Inject(Id = Constants.ProjectilesId)]
        private Transform _projectilesGroup;

        private Rigidbody _rigidbody;

        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;
        public event Action Killed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnter?.Invoke(collision);
        }
        
        private void OnCollisionExit(Collision collision)
        {
            CollisionExit?.Invoke(collision);
        }

        public void TakeDamage()
        {
            Kill();
        }

        public void HandleInput(Vector2 input)
        {
            Move(input.y);
            Rotate(input.x);
        }

        public void Move(float speedMultiplier)
        {
            // Theis method of movement was used to ensure that the units of measurement worked correctly (ignoring mass).
            _rigidbody.MovePosition(_rigidbody.position
                + _rigidbody.rotation * new Vector3(0, 0, speedMultiplier) * (_tankConfig.MoveSpeed * Time.fixedDeltaTime));
        }

        public void Rotate(float speedMultiplier)
        {
            // This method rotation was used to ensure that the units of measurement worked correctly (ignoring mass).
            _rigidbody.MoveRotation(_rigidbody.rotation
                * Quaternion.Euler(0, speedMultiplier * _tankConfig.RotationSpeed * Time.fixedDeltaTime, 0));
        }

        public void Shoot()
        {
            if (!gameObject.activeInHierarchy)
                return;

            Projectile shell = Instantiate(_tankConfig.ShellPrefab, _muzzleTransform.position,
                _muzzleTransform.rotation, _projectilesGroup);

            shell.Hit += TryDamage;
            shell.PushForward(_tankConfig.ShellSpeed, ForceMode.VelocityChange);
        }

        public void Respawn(Pose pose)
        {
            transform.position = pose.position;
            transform.rotation = pose.rotation;

            gameObject.SetActive(true);
        }

        public void Kill()
        {
            gameObject.SetActive(false);

            Killed?.Invoke();
        }

        private void TryDamage(RaycastHit hit)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            damageable?.TakeDamage();
        }
    }
}