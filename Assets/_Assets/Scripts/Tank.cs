using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Tank : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private Transform _muzzleTransform;

        [Inject]
        private TankConfig _tankConfig;
        [Inject(Id = Constants.ProjectilesId)]
        private Transform _projectilesGroup;

        public void Move(Vector2 input)
        {
            // These methods of movement and rotation were used to ensure that the units of measurement worked correctly (ignoring mass).
            _rigidbody.MovePosition(_rigidbody.position
                + _rigidbody.rotation * new Vector3(0, 0, input.y) * (_tankConfig.MoveSpeed * Time.fixedDeltaTime));
            _rigidbody.MoveRotation(_rigidbody.rotation
                * Quaternion.Euler(0, input.x * _tankConfig.RotationSpeed * Time.fixedDeltaTime, 0));
        }

        public void Shoot()
        {
            Projectile shell = Instantiate(_tankConfig.ShellPrefab, _muzzleTransform.position,
                _muzzleTransform.rotation, _projectilesGroup);

            shell.PushForward(_tankConfig.ShellSpeed);
        }
    }
}