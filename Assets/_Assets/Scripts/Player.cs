using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Tank _tank;
        
        [Inject]
        private InputReader _inputReader;

        private void Awake()
        {
            _inputReader.MoveInput += _tank.Move;
            _inputReader.FireInput += _tank.Shoot;
        }
    }
}