using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace TopDownShooter
{
    public class GameManager: Singleton<GameManager>
    {
        [SerializeField]
        private Canvas _mainMenu;

        private PlayerBrain[] players;

        public Transform GetClosestPlayer(Vector3 pos)
        {
            var output = players[0].transform;
            foreach (var player in players)
            {
                if (Vector3.Distance(player.transform.position, pos) <
                    Vector3.Distance(output.position, pos))
                {
                    output = player.transform;
                }
            }
            return output;
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void Start()
        {
            Inititalize();
        }

        private void Inititalize()
        {
            players = FindObjectsByType<PlayerBrain>(FindObjectsSortMode.None);
            _mainMenu?.gameObject.SetActive(false);
            if (_mainMenu != null)
            {
                foreach (PlayerBrain player in players)
                {
                    player.OnPauseEvent.AddListener(ToggleMainMenu);
                }
            }
        }

        private void ToggleMainMenu()
        {
            _mainMenu?.gameObject.SetActive(!_mainMenu.gameObject.activeInHierarchy);
        }
    }
}