using Infrastructure.Services;
using Infrastructure.Services.GameObjectDisposer;
using Infrastructure.Services.InternetCheck;
using UI;

namespace Infrastructure.States
{
    public class NoInternetState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly Curtain _curtain;
        private readonly AllServices _services;
        
        public NoInternetState(GameStateMachine gameStateMachine, AllServices services, Curtain curtain)
        {
            _curtain = curtain;
            _gameStateMachine = gameStateMachine;
            _services = services;
        }

        public void Enter()
        {
            _curtain.ShowCurtain();
            _curtain.SetActiveLoadingText(false);
            _curtain.SetActiveErrorMessage(true);
            
            var internetChecker = _services.Single<IInternetChecker>();
            internetChecker.ConnectionRestoreAttempted += EnterBackToLoadLevelState;
            
            _curtain.InitRestoreConnectionButton(internetChecker);
        }

        public void Exit()
        {
            _curtain.ShowCurtain();
            _curtain.SetActiveLoadingText(true);
            _curtain.SetActiveErrorMessage(false);
        }

        private void EnterBackToLoadLevelState()
        {
            var gameObjectDisposer = _services.Single<IGameObjectRepository>();
            gameObjectDisposer.Dispose();
            
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}