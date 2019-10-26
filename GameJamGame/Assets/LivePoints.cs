using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
	public class LivePoints : MonoBehaviour
	{
		public int maxLivePoints = 100;
		public int LivePointValue;

		CameraController cam;
		// Start is called before the first frame update
		void Start()
		{
			LivePointValue = maxLivePoints;
			cam = FindObjectOfType<CameraController>();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void ReduceLivePoints(int damage)
		{
			LivePointValue -= damage;

			if (LivePointValue <= 0)
			{
				RespawnPlayer();
			}
		}

		public void RespawnPlayer()
		{
			LivePointValue = maxLivePoints;

			PlatformCreator tr = FindObjectOfType<PlatformCreator>();

			transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y - 1, tr.transform.position.z);
		}

	}




}

