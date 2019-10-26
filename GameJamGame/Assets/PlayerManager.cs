using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace GameJam
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]public GameObject[] playerPrefab;
        int playerind;
        const int maxPlayers = 4;

        [SerializeField] List<Transform> playerSpawns = new List<Transform>(4);
        public List<playerController> players = new List<playerController>(maxPlayers);
        // Start is called before the first frame update
        void Start()
        {
            InputManager.OnDeviceDetached += OnDeviceDetached;
        }

        // Update is called once per frame
        void Update()
        {
            var inputDevice = InputManager.ActiveDevice;

            if (Joined(inputDevice))
            {
                if (ThereIsNoPlayerUsingDevice(inputDevice))
                {
                    CreatePlayer(inputDevice);
                    playerind++;
                }
            }
        }
        bool Joined (InputDevice inputDevice)
        {
            return inputDevice.AnyButton.WasPressed;
            

        }
        bool ThereIsNoPlayerUsingDevice ( InputDevice inputDevice)
        {
            return FindPlayerUsingDevice(inputDevice) == null;
        }
        playerController FindPlayerUsingDevice (InputDevice inputDevice)
        {
            var playerCount = players.Count;
            for(var i = 0; i < playerCount; i++)
            {
                var player = players[i];
                if(player.Device == inputDevice)
                {
                    return player;
                }
            }
            return null;
        }
        void OnDeviceDetached (InputDevice inputDevice)
        {
            var player = FindPlayerUsingDevice(inputDevice);
            if(player != null)
            {
                RemovePlayer(player);
            }
        }
        playerController CreatePlayer(InputDevice inputDevice)
        {
            if (players.Count < maxPlayers)
            {
                var playerPosition = playerSpawns[0];
                playerSpawns.RemoveAt(0);
                var gameObject = (GameObject)Instantiate(playerPrefab[playerind], playerPosition.position, Quaternion.identity);
                var player = gameObject.GetComponent<playerController>();
                player.Device = inputDevice;
                players.Add(player);

                return player;
            }

            return null;
        }
        void RemovePlayer(playerController player)
        {
            playerSpawns.Insert(0, player.transform);
            players.Remove(player);
            player.Device = null;
            Destroy(player.gameObject);
        }
    }
}
