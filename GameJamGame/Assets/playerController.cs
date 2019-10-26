using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace GameJam
{
    public class playerController : MonoBehaviour
    {
        public InputDevice Device { get; set; }
        public float moveSpeed = 5;
        bool isGroundet = false;
        Rigidbody2D rig;
        Collider2D col;
        public Vector3 movement;
        bool hasJumped;
        bool hasDashed;

        public float JumpPower = 5;
        public float dashForce = 100;

        // Start is called before the first frame update
        void Start()
        {
            rig = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            movement = new Vector3(Device.LeftStick.X, Device.LeftStick.Y, 0);

            Jump();
            Movement();
            if (Device.RightTrigger.WasPressed)
            {
                Dash();
            }

        }

        private void Movement()
        {
            transform.position += new Vector3(movement.x, 0, 0) * Time.deltaTime * moveSpeed;
        }

        void Jump()
        {
            if (Device.Action1.WasPressed && isGroundet == true)
            {
                hasJumped = true;
                rig.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
            }

        }
        int DashInd = 0;
        private int dashNumbers = 1;
        void Dash()
        {
            if (DashInd >= dashNumbers)
            {
                return;

            }
            Debug.Log("Dash");
            rig.AddForce(new Vector2(Device.LeftStick.Value.x, Device.LeftStick.Value.y)*dashForce, ForceMode2D.Impulse);
            DashInd++;
        }

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Dead")
			{
				print("feerfererfefefefeferfere");
				GetComponent<LivePoints>().RespawnPlayer();
			}
		}

		private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                DashInd = 0;
                hasDashed = false;
                isGroundet = true;
			}


        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                Rigidbody2D heR = collision.transform.GetComponent<Rigidbody2D>();
                GameObject heG = collision.gameObject ;

                if(transform.position.y < heG.transform.position.y)
                {
                    heG.transform.position = transform.position;
                    transform.position = heG.transform.position;

                    heR.AddForce(new Vector2(0,-1));
                    rig.AddForce(new Vector2(0,1));
                    
                }
                    
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                hasJumped = false;
                isGroundet = false;
            }
        }


    }
}