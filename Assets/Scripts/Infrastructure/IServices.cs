using Data;
using Level;
namespace Infrastructure
{
    public interface IServices
    {
        public float ScreenWidth { get; }
        public void InitRandomizer(GameSettings gameSettings);
        
        public (int currentLevel, int currentScore) GetCurrentLevelAndScore();
        public void SaveCurrentLevel(int level, int score);

        public void CreateNewSpritePool();
        public SpritePool SpritePool { get; }
        public void StartGame();
    }
}
