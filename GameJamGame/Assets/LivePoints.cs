using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
	public class LivePoints : MonoBehaviour
	{
		public GameObject Panel;
		public List<GameObject> herzen = new List<GameObject>();

		public int maxLivePoints = 3;
		public int LivePointValue;

		public GameObject Herz;


		CameraController cam;
		// Start is called before the first frame update
		void Start()
		{
			LivePointValue = maxLivePoints;
			cam = FindObjectOfType<CameraController>();
			UpdateLiveImage();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void ReduceLivePoints(int damage)
		{
			LivePointValue -= damage;
		}

		public void RespawnPlayer(Transform pos)
		{
			ReduceLivePoints(1);
			UpdateLiveImage();
			if (LivePointValue <= 0)
			{
				FindObjectOfType<PlayerManager>().DeletePLayer(GetComponent<playerController>());
				Destroy(gameObject);
				return;
			}

			PlatformCreator tr = FindObjectOfType<PlatformCreator>();

			transform.position = pos.position;
		}

		public void UpdateLiveImage()
		{

			for (int i = 0; i < herzen.Count; i++)
			{
				herzen[i].SetActive(false);
			}

			for (int i = 0; i < LivePointValue; i++)
			{
				herzen[i].SetActive(true);
			}

		}



	}




}

