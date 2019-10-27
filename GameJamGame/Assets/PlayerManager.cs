using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace GameJam
{
	public class PlayerManager : MonoBehaviour
	{
		public AudioClip win;
		public TextMeshProUGUI winText;
		public Image winImage;

		[SerializeField] public GameObject[] playerPrefab;
		int playerind;
		const int maxPlayers = 4;

		[SerializeField] List<Transform> playerSpawns = new List<Transform>(4);
		public List<playerController> players = new List<playerController>(maxPlayers);

		public bool GameisStarted = false;


		// Start is called before the first frame update
		void Start()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;
		}

		// Update is called once per frame
		void Update()
		{
			if (players.Count <= 1 && GameisStarted)
			{
				StartCoroutine(PlayerWin());
			}

			var inputDevice = InputManager.ActiveDevice;

			if (Joined(inputDevice))
			{
				if (ThereIsNoPlayerUsingDevice(inputDevice))
				{
					CreatePlayer(inputDevice);
					playerind++;
				}
			}

			if (!GameisStarted)
			{
				int rdyCounter = 0;

				for (int i = 0; i < players.Count; i++)
				{

					if (players[i].playerReady)
					{
						rdyCounter++;
					}
				}

				if (rdyCounter == players.Count && players.Count != 0)
				{
					StartGame();
				}
			}

		}

		IEnumerator PlayerWin()
		{
			winText.gameObject.SetActive(true);
			winImage.sprite = players[0].gameObject.GetComponent<SpriteRenderer>().sprite;
			AudioSource.PlayClipAtPoint(win, transform.position);

			winImage.gameObject.SetActive(true);


			yield return new WaitForSeconds(2);

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		}

		private void StartGame()
		{
			GameisStarted = true;

			for (int i = 0; i < players.Count; i++)
			{
				players[i].GameisStarted = true;
			}
		}

		bool Joined(InputDevice inputDevice)
		{
			return inputDevice.Action1.WasPressed;
		}


		bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
		{
			return FindPlayerUsingDevice(inputDevice) == null;
		}
		playerController FindPlayerUsingDevice(InputDevice inputDevice)
		{
			var playerCount = players.Count;
			for (var i = 0; i < playerCount; i++)
			{
				var player = players[i];
				if (player.Device == inputDevice)
				{
					return player;
				}
			}
			return null;
		}
		void OnDeviceDetached(InputDevice inputDevice)
		{
			var player = FindPlayerUsingDevice(inputDevice);
			if (player != null)
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
				print("fewfewewffwf " + player);
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


		public void DeletePLayer(playerController player)
		{

			players.Remove(player);

		}

	}
}
