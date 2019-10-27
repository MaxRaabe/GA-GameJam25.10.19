using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
	public GameObject BottumPlane;
	public GameObject SpawnObjects;

	public int PlattformCount = 3;

	private void Start()
	{
		StartCoroutine(SpawnRoutine());
	}

	IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(0.01f);

		float dis;
		
		while (true)
		{
			dis = (transform.position - BottumPlane.transform.position).magnitude;
			
			if (dis < 30)
			{
				SpawnObject();
				transform.position += new Vector3(0,5,0);
			}
			yield return new WaitForSeconds(0.1f);
		}

	}

	[SerializeField]
	private float _radius = 5;

	protected Vector3 CalculatePossibleSpawnPoints()
	{
		//float ran = UnityEngine.Random.Range(5, 10);
		Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _radius;
		randomPoint.z = 0;
		randomPoint.y = 0;

		Vector3 spawnPoints  = this.transform.position + randomPoint;

		return spawnPoints;
	}


	void SpawnObject()
	{
		int index = 0;
		for (int i = 0; i < PlattformCount; i++)
		{
			bool isCollide = true;
			while (isCollide)
			{
				Vector3 pos = CalculatePossibleSpawnPoints();
				Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(pos.x, pos.y), 0.3f);

				if (hitColliders == null)
				{
					Instantiate(SpawnObjects, pos, Quaternion.identity);
					isCollide = false;
				}
				index++;

				if (index >= 100)
				{
					isCollide = false;
				}
			}
		}
	}
}
