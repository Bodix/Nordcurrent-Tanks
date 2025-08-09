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
        private AIBehaviour _aiBehaviour;

        [Header("Configs")]
        [SerializeField]
        private GameConfig _gameConfig;
        [SerializeField]
        private TankConfig _tankConfig;
        [SerializeField]
        private AIConfig _aiConfig;

        [Header("Groups")]
        [SerializeField]
        private Transform _botsGroup;
        [SerializeField]
        private Transform _projectilesGroup;

        public override void InstallBindings()
        {
            Container.BindInstance(_player);
            Container.Bind<AIBehaviour>().FromMethod(() => Container.InstantiateUnityObject(_aiBehaviour));

            Container.BindInstance(Instantiate(_gameConfig));
            Container.BindInstance(Instantiate(_tankConfig));
            Container.BindInstance(Instantiate(_aiConfig));

            Container.BindInstance(_botsGroup).WithId(Constants.BotsId);
            Container.BindInstance(_projectilesGroup).WithId(Constants.ProjectilesId);
        }
    }
}