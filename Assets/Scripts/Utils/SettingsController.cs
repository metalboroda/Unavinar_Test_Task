using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class SettingsController : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}