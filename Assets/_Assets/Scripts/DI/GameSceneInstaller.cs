using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.DI
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Player _player;
        [SerializeField]
        private GameConfig _gameConfig;
        [SerializeField]
        private TankConfig _tankConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_player);
            Container.BindInstance(_gameConfig);
            Container.BindInstance(_tankConfig);
        }
    }
}