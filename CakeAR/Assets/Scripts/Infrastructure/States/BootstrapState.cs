using Infrastructure.Services;
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
            _curtain.ShowCurtain(true);
            _curtain.SetActiveLoadingText(true);
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetLoader>(new AssetLoader());
        }
    }
}