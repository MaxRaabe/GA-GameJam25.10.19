using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
	public float moveSpeed = 5;
	bool isGroundet = false;
	Rigidbody2D rig;
	Collider2D col;
	Vector3 movement;

	public float JumpPower = 5;

	// Start is called before the first frame update
	void Start()
    {
		rig = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

		Jump();
		Movement();
	}

	private void Movement()
	{
		transform.position += movement * Time.deltaTime * moveSpeed;
	}

	void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGroundet == true)
		{
			rig.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
			isGroundet = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
			isGroundet = false;
		}
	}


}
