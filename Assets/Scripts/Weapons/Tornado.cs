using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Tornado : ShotProjectile
    {
        bool startCircle = false;
        Vector3 circlePos;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }

            /*if (Vector3.Distance(player.transform.position, transform.position) > 5 && !startCircle)
            {
                startCircle = true;
                circlePos = transform.position;
            }*/
            if (startCircle)
            {
                Circle();
            }
            transform.rotation = Quaternion.identity;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            if (!startCircle)
            {
                //defines circling position as 2 units towards the player from the position of the tornado
                circlePos = player.transform.position - transform.position;
                circlePos.Normalize();
                circlePos = transform.position + new Vector3(circlePos.x * 2, circlePos.y * 2);
            }
            startCircle = true;
        }

        float RotationSpeed = 1;

        float CircleRadius = 2;

        float ElevationOffset = 1;

        private Vector3 positionOffset;
        private float angle;

        //tornado always starts at angle = 0. need to start at angle = angle between tornado position and circle position (may need to use atan2)
        private void Circle()
        {
            positionOffset.Set(Mathf.Cos(angle) * CircleRadius, Mathf.Sin(angle) * CircleRadius,1 );
            transform.position = circlePos + positionOffset;
            angle += Time.deltaTime * movespeed*.8f;
        }
    }
}