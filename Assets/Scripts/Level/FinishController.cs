using Assets.Scripts.Managers;
using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class FinishController : MonoBehaviour
    {
        [Header("FX")]
        [SerializeField]
        private List<GameObject> finishFxList = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerMovement = other.GetComponent<PlayerMovement>();

                if (playerMovement != null)
                    playerMovement.CanMoveSwitch(false);

                GameHandler.Instance.UpdateGameState(GameState.Win);

                EnableFinsihFx();
            }
        }

        private void EnableFinsihFx()
        {
            foreach (var i in finishFxList)
            {
                i.SetActive(true);
            }
        }
    }
}