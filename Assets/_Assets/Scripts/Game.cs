using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private AIPlayer _aiPlayerPrefab;

        [Inject]
        private DiContainer _container;
        [Inject]
        private GameConfig _gameConfig;
        [Inject(Id = Constants.BotsId)]
        private Transform _botsGroup;

        private void Start()
        {
            for (int i = 0; i < _gameConfig.BotsCount; i++)
                _container.InstantiatePrefabForComponent<AIPlayer>(_aiPlayerPrefab, _botsGroup).RandomizePosition();
        }
    }
}