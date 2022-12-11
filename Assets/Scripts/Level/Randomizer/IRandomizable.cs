using Infrastructure;
namespace Level.Randomizer

{
    public interface IRandomizable : IService
    {
        public RandomData GetRandomData();
    }
}
