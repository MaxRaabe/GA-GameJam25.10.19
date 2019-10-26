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

        public float JumpPower = 5;
        public float dashForce;

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
            Dash();
        }

        private void Movement()
        {
            transform.position += new Vector3(movement.x, 0, 0) * Time.deltaTime * moveSpeed;
        }

        void Jump()
        {
            if (Device.Action1.WasPressed && isGroundet == true)
            {
                rig.AddForce(new Vector2(0, JumpPower), ForceMode2D.Impulse);
                hasJumped = true;
            }

        }
        void Dash()
        {
            if (hasJumped)
            {
                if (Device.Action1.WasPressed)
                {
                    Debug.Log("Dash towards :" + Device.LeftStick.X * dashForce);
                    rig.AddForce(new Vector2(1,1) * dashForce, ForceMode2D.Impulse);
                }
            }

        }

        private void OnCollisionStay2D(Collision2D collision)
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
                hasJumped = false;
                isGroundet = false;
            }
        }


    }
}