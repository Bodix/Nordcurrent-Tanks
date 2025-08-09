using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private AIBot _botPrefab;
        [Inject(Id = Constants.BotsId)]
        private Transform _botsGroup;

        [Inject]
        private GameConfig _gameConfig;

        private void Start()
        {
            for (int i = 0; i < _gameConfig.BotsCount; i++)
                Instantiate(_botPrefab, _botsGroup).Respawn();
        }
    }
}