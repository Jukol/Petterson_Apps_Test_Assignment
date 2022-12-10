using Infrastructure;
using UnityEngine;
namespace Level.Visual
{
    public class HudManager : MonoBehaviour
    {
        [SerializeField] private LevelIntro levelIntro;
        [SerializeField] private ScoreVisual scoreVisual;
        [SerializeField] private ScoreTracker scoreTracker;

        public void Init(Services services)
        {
            levelIntro.Init(services);
            scoreTracker.Init(services);
            scoreVisual.Init(services);
        }
    }
}
