using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class BotSpawner : MonoBehaviour
    {
        [SerializeField]
        private AIBot _botPrefab;

        [Inject]
        private GameConfig _gameConfig;

        private void Awake()
        {
            for (int i = 0; i < _gameConfig.BotsCount; i++)
            {
                Instantiate(_botPrefab);
            }
        }
    }
}