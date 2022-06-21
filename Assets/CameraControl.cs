using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class CameraControl : MonoBehaviour
    {

        public GameObject target;
        Vector3 targetPos;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            targetPos = target.transform.position;
            targetPos.z = transform.position.z;
            transform.position = targetPos;

        }
    }
}