using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public float Lifetime = 5f;
        public float ColliderRadius = 0.5f;
        public LayerMask LayerMask = 1; // 1 = Default

        private Rigidbody _rigidbody;

        public event Action<RaycastHit> Hit;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Destroy(gameObject, Lifetime);
        }

        private void FixedUpdate()
        {
            CheckHit();
        }

        public void PushForward(float speed, ForceMode mode = ForceMode.Impulse)
        {
            Push(transform.forward, speed, mode);
        }

        public void Push(Vector3 direction, float speed, ForceMode mode = ForceMode.Impulse)
        {
            _rigidbody.rotation = Quaternion.LookRotation(direction);
            _rigidbody.AddForce(direction * speed, mode);
        }

        private void CheckHit()
        {
            Vector3 direction = _rigidbody.velocity.normalized;
            float magnitudeDelta = _rigidbody.velocity.magnitude * Time.deltaTime;

            if (Physics.SphereCast(transform.position, ColliderRadius, direction,
                    out RaycastHit hit, magnitudeDelta, LayerMask))
            {
                Destroy(gameObject);

                Hit?.Invoke(hit);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, ColliderRadius);
        }
    }
}