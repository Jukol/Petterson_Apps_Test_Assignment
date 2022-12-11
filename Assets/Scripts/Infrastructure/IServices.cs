using Data;
namespace Infrastructure
{
    public interface IServices
    {
        public void InitRandomizer(GameSettings gameSettings);
        public void StartGame();
    }
}
