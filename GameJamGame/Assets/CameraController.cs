using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GameJam
{
    public class CameraController : MonoBehaviour
    {
        public List<playerController> players = new List<playerController>();
        public GameObject player;
        public CinemachineTargetGroup group;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
            //print("screenPos " + screenPos);
            //group.m_Targets[1].weight = 2;

            var v1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 1));
            //var v2 = Camera.main.ViewportToWorldPoint(player.transform.position);
            var v2 = Camera.main.WorldToScreenPoint(player.transform.position);

            //var v2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 1));
            var dist = Vector3.Distance(v1, v2);

            //print("000   v1         " + v1);
            //print("000    v2        " + v2);
        }
    }
}
