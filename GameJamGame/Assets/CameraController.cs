using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GameJam
{
	public class CameraController : MonoBehaviour
	{
		public CinemachineTargetGroup group;
		public Transform CameraPoint;
		public Transform UnderBorder;
		PlayerManager players;


		// Start is called before the first frame update
		void Start()
		{
			players = this.GetComponent<PlayerManager>();
		}
		float maxDis = 0;

		// Update is called once per frame
		void Update()
		{
			Vector2 erg = Vector2.zero;
			for (int i = 0; i < players.players.Count; i++)
			{
				if (players.players[i].transform.position.y < UnderBorder.position.y)
				{
					players.players[i].GetComponent<LivePoints>().RespawnPlayer();
				}

				float dis = (players.players[i].transform.position - UnderBorder.position).magnitude;

				if (dis > maxDis)
				{
					maxDis = dis;
				}

				float _x = players.players[i].transform.position.x;
				float _y = players.players[i].transform.position.y;
				erg += new Vector2( _x, _y);

			}

			if (players.players.Count == 0)
			{
				return;
			}

			erg = erg / players.players.Count;

			if (erg.y < CameraPoint.position.y)
			{
				erg.y = CameraPoint.position.y;
			}

			maxDis = 0;

			if (players.GameisStarted)
			{
				erg.y = erg.y + Time.deltaTime*100;
			}
			//CameraPoint.position = new Vector3(erg.x , erg.y, 0);

			CameraPoint.position = Vector3.Lerp(CameraPoint.position, new Vector3(erg.x, erg.y, 0), 3 * Time.deltaTime);
		}

	}
}
