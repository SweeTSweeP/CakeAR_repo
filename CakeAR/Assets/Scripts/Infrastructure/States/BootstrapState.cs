using Infrastructure.Services;
using Infrastructure.Services.GameObjectDisposer;
using Infrastructure.Services.InternetCheck;
using Infrastructure.Services.Loaders.AssetLoader;
using UI;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;
        private readonly Curtain _curtain;

        public BootstrapState(
            GameStateMachine gameStateMachine,
            AllServices services,
            Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;
            _curtain = curtain;
        }

        public void Enter()
        {
            RegisterServices();
            _curtain.ShowCurtain();
            _curtain.SetActiveLoadingText(true);
            _curtain.SetActiveErrorMessage(false);
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetLoader>(new AssetLoader());
            _services.RegisterSingle<IInternetChecker>(new InternetChecker());
            _services.RegisterSingle<IGameObjectRepository>(new GameObjectRepository());
        }
    }
}