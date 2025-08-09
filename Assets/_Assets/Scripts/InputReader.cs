using System;
using UnityEngine;

namespace Game
{
    public class InputReader : MonoBehaviour
    {
        private MainInputActions _inputActions;

        public event Action<Vector2> MoveInput;

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

        private void FixedUpdate()
        {
            Vector2 moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            MoveInput?.Invoke(moveInput);
        }
    }
}