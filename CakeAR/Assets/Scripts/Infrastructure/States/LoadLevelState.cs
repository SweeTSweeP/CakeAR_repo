using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.Services.GameObjectDisposer;
using Infrastructure.Services.InternetCheck;
using Infrastructure.Services.Loaders.AssetLoader;
using Infrastructure.Services.Loaders.SceneLoader;
using UI;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly Curtain _curtain;
        private readonly AllServices _services;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            Curtain curtain,
            AllServices services,
            SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
            _services = services;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            var internetChecker = _services.Single<IInternetChecker>();

            internetChecker.StartCheckInternetConnection();
            internetChecker.ConnectionLost += () => EnterNoConnectionState(Constants.FailedConnection, true);

            _sceneLoader.Load(Constants.MainSceneName, EnterLoadLevel);
        }

        public void Exit() =>
            _curtain.HideCurtain();

        private async void EnterLoadLevel()
        {
            var assetLoader = _services.Single<IAssetLoader>();
            var gameObjectDisposer = _services.Single<IGameObjectDisposer>();

            var cakeTask = assetLoader.LoadModel(Constants.FakeUrl, "Cake", gameObjectDisposer);

            await UniTask.WaitWhile(() => !cakeTask.GetAwaiter().IsCompleted);

            var result = await cakeTask;

            if (TryToPreventWrongConnection(result)) return;

            _gameStateMachine.Enter<GameLoopState>();
        }

        private bool TryToPreventWrongConnection(bool result)
        {
            if (result) return false;
            
            EnterNoConnectionState(Constants.WrongURL, false);
            
            return true;
        }

        private void EnterNoConnectionState(string errorMessage, bool buttonActiveStatus)
        {
            _curtain.SetErrorMessage(errorMessage, buttonActiveStatus);
            _gameStateMachine.Enter<NoInternetState>();
        }
    }
}