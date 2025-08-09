using UnityEngine;
using Zenject;

namespace Game
{
    public class InputReader : MonoBehaviour
    {
        [Inject]
        private MainInputActions _inputActions;
    }
}