namespace Infrastructure.States
{
    public class EntryPointState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private const string Boot = "Boot";
        private const string Level1 = "Level1";

        public EntryPointState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Enter() => 
            _sceneLoader.Load(Boot, LoadLevel1);
        private void LoadLevel1() => 
            _sceneLoader.Load(Level1);
        public void Exit()
        {
        }
    }
}
