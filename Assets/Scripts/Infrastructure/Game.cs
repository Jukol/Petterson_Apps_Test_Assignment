using Infrastructure.States;
namespace Infrastructure
{
    public class Game
    {
        public readonly StateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new StateMachine(new SceneLoader(coroutineRunner));
        }
    }
}
