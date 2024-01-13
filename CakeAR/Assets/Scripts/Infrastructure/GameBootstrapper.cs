using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Curtain curtain;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(curtain);
            _game.stateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}