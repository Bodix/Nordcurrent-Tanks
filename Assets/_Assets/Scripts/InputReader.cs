using System;
using UnityEngine;

namespace Game
{
    public class InputReader : MonoBehaviour
    {
        private MainInputActions _inputActions;

        public event Action<Vector2> MoveInput;
        public event Action FireInput;

        private void Awake()
        {
            _inputActions = new MainInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Update()
        {
            bool fireInput = _inputActions.Player.Fire.WasPressedThisFrame();
            if (fireInput)
                FireInput?.Invoke();
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            if (moveInput != Vector2.zero)
                MoveInput?.Invoke(moveInput);
        }
    }
}