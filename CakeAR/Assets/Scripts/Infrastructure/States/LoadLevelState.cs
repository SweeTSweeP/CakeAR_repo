using Cysharp.Threading.Tasks;
using Infrastructure.Services;
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
            _sceneLoader.Load(Constants.MainSceneName, EnterLoadLevel);
        }

        public void Exit() => 
            _curtain.HideCurtain();

        private async void EnterLoadLevel()
        {
            var assetLoader = _services.Single<IAssetLoader>();

            var task = assetLoader.LoadModel(Constants.CakeModelUrl, "Cake");

            await UniTask.WaitWhile(() => task.Status == UniTaskStatus.Pending);
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}