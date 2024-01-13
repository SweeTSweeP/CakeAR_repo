using Infrastructure.Services;
using UI;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;
        private readonly Curtain _curtain;

        public GameLoopState(
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
            
        }

        public void Exit()
        {
            _curtain.ShowCurtain(true);
        }
    }
}