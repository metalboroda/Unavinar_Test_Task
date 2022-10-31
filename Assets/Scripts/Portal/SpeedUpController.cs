using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Portal
{
    public class SpeedUpController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerMovement = other.GetComponent<PlayerMovement>();

                if (playerMovement != null)
                    playerMovement.CanSpeedUpSwitch(true);
            }
        }
    }
}