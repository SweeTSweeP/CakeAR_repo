using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.Loaders.SceneLoader;
using UI;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, Curtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, curtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, curtain, services, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this, services, curtain),
                [typeof(NoInternetState)] = new NoInternetState(this, services, curtain),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();

            var state = GetState<TState>();

            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}