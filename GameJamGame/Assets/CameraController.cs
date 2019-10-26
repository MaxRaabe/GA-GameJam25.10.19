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
		public Transform[] playersTransforms;

		public Queue<Transform> playerTransforms = new Queue<Transform>();

		// Start is called before the first frame update
		void Start()
		{
			players = this.GetComponent<PlayerManager>();
			playerTransforms.Clear();
		}

		float maxDis = 0;
		// Update is called once per frame
		void Update()
		{
			Vector2 erg = Vector2.zero;
			for (int i = 0; i < players.players.Count; i++)
			{
				//float dis = (players.players[i].transform.position - UnderBorder.position).magnitude;

				//if (dis > maxDis)
				//{
				//	playerTransforms.Enqueue(players.players[i].transform);

				//	maxDis = dis;
				//	CameraPoint.position = (players.players[i].transform.position);
				//}

				float _x = players.players[i].transform.position.x;
				float _y = players.players[i].transform.position.y;

				erg += new Vector2( _x, _y);
				print("wdwdwd"+ erg);
			}

			if (players.players.Count == 0)
			{
				return;
			}

			erg = erg / players.players.Count;

			CameraPoint.position = new Vector3(erg.x , erg.y, 0);

			playerTransforms.Clear();

			//	maxDis = 0;

			//Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
			//print("screenPos " + screenPos);
			//group.m_Targets[1].weight = 2;

			//var v1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 1));
			//var v2 = Camera.main.ViewportToWorldPoint(player.transform.position);
			//var v2 = Camera.main.WorldToScreenPoint(player.transform.position);

			//var v2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 1));
			//var dist = Vector3.Distance(v1, v2);

			//print("000   v1         " + v1);
			//print("000    v2        " + v2);
		}
	}
}
