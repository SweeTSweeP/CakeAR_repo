using Infrastructure.Services;
using Infrastructure.Services.Loaders.SceneLoader;
using Infrastructure.States;
using UI;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine stateMachine;

        public Game(Curtain curtain) =>
            stateMachine = new GameStateMachine(
                new SceneLoader(),
                curtain, 
                AllServices.Container);

    }
}