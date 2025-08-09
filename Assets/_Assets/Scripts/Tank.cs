using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Tank : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [Inject]
        private TankConfig _tankConfig;

        public void Move(Vector2 input)
        {
            _rigidbody.MovePosition(_rigidbody.position + _rigidbody.rotation * new Vector3(0, 0, input.y) * (_tankConfig.MoveSpeed * Time.fixedDeltaTime));
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, input.x * _tankConfig.RotationSpeed * Time.fixedDeltaTime, 0));
        }
    }
}