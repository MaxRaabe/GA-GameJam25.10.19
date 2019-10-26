using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
	public class LivePoints : MonoBehaviour
	{
		public GameObject Panel;
		List<GameObject> herzen = new List<GameObject>();

		public int maxLivePoints = 3;
		public int LivePointValue;

		public GameObject Herz;


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
		}

		public void RespawnPlayer()
		{
			ReduceLivePoints(1);
			if (LivePointValue <= 0)
			{
				FindObjectOfType<PlayerManager>().DeletePLayer(GetComponent<playerController>());
				Destroy(gameObject);
				return;
			}

			PlatformCreator tr = FindObjectOfType<PlatformCreator>();

			transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y - 5, tr.transform.position.z);
		}

		public void UpdateLiveImage()
		{

		}



	}




}

