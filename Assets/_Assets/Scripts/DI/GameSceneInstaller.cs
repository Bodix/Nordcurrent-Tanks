using UnityEngine;
using Zenject;

namespace Game.DI
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Player _player;

        public override void InstallBindings()
        {
            Container.BindInstance(_player);
        }
    }
}