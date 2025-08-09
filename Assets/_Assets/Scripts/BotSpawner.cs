using Evolutex.Evolunity.Extensions;
using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class BotSpawner : MonoBehaviour
    {
        [SerializeField]
        private AIBot _botPrefab;
        [SerializeField]
        private Vector2 _spawnLimits;

        [Inject]
        private GameConfig _gameConfig;

        private void Awake()
        {
            for (int i = 0; i < _gameConfig.BotsCount; i++)
            {
                Vector3 position = Vector3.zero.Randomize(
                    (-_spawnLimits.x / 2, _spawnLimits.x / 2),
                    (0, 0),
                    (-_spawnLimits.y / 2, _spawnLimits.y / 2));

                Instantiate(_botPrefab, position, Quaternion.identity);
            }
        }
    }
}