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
		private bool isJumpimg = false;
		bool hasJumped;
        bool hasDashed;
        bool isDashing;

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
            if (Device.Action1.WasPressed && isGroundet == true && isJumpimg == false)
            {
				StartCoroutine(deactivationJumpBool());
				isJumpimg = true;
                hasJumped = true;
                rig.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
            }

        }

		IEnumerator deactivationJumpBool()
		{
			yield return new WaitForSeconds(0.5f);
			isJumpimg = false;
			yield return null;
		}

        int DashInd = 0;
        private int dashNumbers = 1;
        void Dash()
        {
            if (!isDashing)
            {
                if (DashInd >= dashNumbers)
                {
                    return;

                }
                isDashing = true;
                Debug.Log("Dash");
                rig.AddForce(new Vector2(Device.LeftStick.Value.x, Device.LeftStick.Value.y) * dashForce, ForceMode2D.Impulse);
                DashInd++;
                StartCoroutine(deactivateDash());
            }
        }
        IEnumerator deactivateDash()
        {
            yield return new WaitForSeconds(1.0f);
            isDashing = false;
            yield return null;
        }

		//private void OnTriggerEnter2D(Collider2D collision)
		//{
		//	if (collision.tag == "Dead")
		//	{
		//		print("feerfererfefefefeferfere");
		//		GetComponent<LivePoints>().RespawnPlayer();
		//	}
		//}

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

                    heR.velocity = new Vector2(0,0);
                    heR.AddForce(new Vector2(0,-10));
                    rig.AddForce(new Vector2(0,10));
                    
                }
                else
                {
                    return;
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