using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.DI
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Player _player;

        [Header("Configs")]
        [SerializeField]
        private GameConfig _gameConfig;
        [SerializeField]
        private TankConfig _tankConfig;

        [Header("Groups")]
        [SerializeField]
        private Transform _botsGroup;
        [SerializeField]
        private Transform _projectilesGroup;

        public override void InstallBindings()
        {
            Container.BindInstance(_player);

            Container.BindInstance(Instantiate(_gameConfig));
            Container.BindInstance(Instantiate(_tankConfig));

            Container.BindInstance(_botsGroup).WithId(Constants.BotsId);
            Container.BindInstance(_projectilesGroup).WithId(Constants.ProjectilesId);
        }
    }
}