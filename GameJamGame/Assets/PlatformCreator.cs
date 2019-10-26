using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
	public GameObject BottumPlane;
	public GameObject SpawnObjects;

	private void Start()
	{
		//StartCoroutine(SpawnRoutine());
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(0.2f);

		float dis;

		while (true)
		{
			dis = (transform.position - BottumPlane.transform.position).magnitude;

			if (dis < 10)
			{
				SpawnObject();
				transform.position += new Vector3(1,0,0);
			}
			yield return new WaitForSeconds(0.2f);
		}

	}

	[SerializeField]
	private float _radius = 5;

	protected Vector3 CalculatePossibleSpawnPoints()
	{
		Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _radius;
		randomPoint.z = 0;
		randomPoint.y = 0;

		Vector3 spawnPoints  = this.transform.position + randomPoint;

		return spawnPoints;
	}

	void SpawnObject()
	{
		Instantiate(SpawnObjects, CalculatePossibleSpawnPoints(), Quaternion.identity);
	}


}
