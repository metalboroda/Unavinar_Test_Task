using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Instance;

        public int levelIndex;

        private void Awake()
        {
            Instance = this;
        }
    }
}