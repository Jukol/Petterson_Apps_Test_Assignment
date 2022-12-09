using Data;
namespace Infrastructure
{
    public interface IServices
    {
        public void InitRandomizer(LevelsData levelsData);
        public void StartGame();
    }
}
