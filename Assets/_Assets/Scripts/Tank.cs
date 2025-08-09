using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    [RequireComponent(typeof(BoxCollider))]
    public class Tank : MonoBehaviour
    {
        [Inject]
        private TankConfig _tankConfig;

        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public void Move(Vector2 input)
        {
            transform.Translate(new Vector3(0, 0, input.y) * (_tankConfig.MoveSpeed * Time.fixedDeltaTime));
            transform.Rotate(Vector3.up, input.x * _tankConfig.RotationSpeed * Time.fixedDeltaTime);
        }
    }
}