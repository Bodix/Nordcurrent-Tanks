using System;
using NaughtyAttributes;
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

        public void PushForward(float speed)
        {
            Push(transform.forward, speed);
        }

        public void Push(Vector3 direction, float speed)
        {
            _rigidbody.rotation = Quaternion.LookRotation(direction);
            _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
        }

        private void CheckHit()
        {
            Vector3 direction = _rigidbody.velocity;
            if (_rigidbody.useGravity)
                direction += Physics.gravity * Time.deltaTime;
            direction = direction.normalized;

            float velocityMagnitudeDelta = _rigidbody.velocity.magnitude * Time.deltaTime;

            if (Physics.SphereCast(transform.position, ColliderRadius, direction,
                    out RaycastHit hit, velocityMagnitudeDelta, LayerMask))
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