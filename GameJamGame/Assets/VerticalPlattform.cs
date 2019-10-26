using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlattform : MonoBehaviour
{

	private PlatformEffector2D effector;
	public float waitTime;
	public bool playerOn = false;
	playerController controller;

	// Start is called before the first frame update
	void Start()
	{
		effector = GetComponent<PlatformEffector2D>();
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			waitTime = 0.5f;
			effector.rotationalOffset = 0;
		}

		if (playerOn)
		{
			if (controller.movement.y < 0)
			{
				if (waitTime <= 0)
				{
					effector.rotationalOffset = 180f;
					waitTime = 0.5f;
				}
				else
				{
					waitTime -= Time.deltaTime;

				}
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				effector.rotationalOffset = 0;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			controller = collision.transform.GetComponent<playerController>();
			playerOn = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			playerOn = false;
			controller = null;
		}
	}
}
