using Zenject;

namespace Game.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputReader>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}